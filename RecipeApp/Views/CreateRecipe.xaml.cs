using System;
using System.Collections.Generic;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipeApp.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecipeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateRecipe : ContentPage
    {
        DataBase db = new DataBase();
        private Recipe recipe { get; set; }
        private ObservableCollection<RecipeStep> Steps { get; set; }
        private byte[] mainPhotoBytes { get; set; }
        private List<byte[]> imgBytes { get; set; }
        private ObservableCollection<Ingredient> ingredients { get; set; }
        private List<Product> products { get; set; }
        private List<string> countType = new List<string>() { "штук", "гр", "кг", "ст.лож." };
        private List<string> timeType = new List<string>() { "минут", "часов", "дней" };

        public CreateRecipe()
        {
            InitializeComponent();
            recipe = new Recipe();
            Steps = new ObservableCollection<RecipeStep>();
            ingredients = new ObservableCollection<Ingredient>();
            imgBytes = new List<byte[]>();
            CollectionViewRecipeSteps.ItemsSource = Steps;
            CollectionViewIngredients.ItemsSource = ingredients;

            PickerCountType.ItemsSource = countType;
            PickerCountType.SelectedItem = countType[0];

            PickerTimeType.ItemsSource = timeType;
            PickerTimeType.SelectedItem = timeType[0];

            object obj = "";
            if (Application.Current.Properties.TryGetValue("ProductsList", out obj)) ;
            products = obj as List<Product>;
            IngredientsPicker.ItemsSource = products;
            IngredientsPicker.SelectedItem = products[0];

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!(Application.Current.Properties.ContainsKey("MyProfile") && Application.Current.Properties["MyProfile"] != null))
            {
                await DisplayAlert("", "Для добавление рецепта войдите в свою учетную запись", "Ок");
                Shell appshell = Application.Current.MainPage as Shell;
                await appshell.GoToAsync("app://RecipeApp.AppShell/profileTab", false);
            }
        }

        private void UploadRecipe()
        {
            for (int i = 0; i < Steps.Count; i++)
            {
                if (imgBytes[i] != null)
                {
                    string url = db.UploadPhoto(imgBytes[i]);
                    if (url == null)
                    {
                        DisplayAlert("Ошибка", "Не удалось создать рецепт, попробуйте еще раз", "Ок");
                        return;
                    }
                    Steps[i].img_url = url;
                    Steps[i].image = null;
                }
            }
            if (mainPhotoBytes != null)
            {
                string url = db.UploadPhoto(mainPhotoBytes);
                if (url == null)
                {
                    DisplayAlert("Ошибка", "Не удалось создать рецепт, попробуйте еще раз", "Ок");
                    return;
                }
                recipe.img_url = url;
                recipe.image = null;
            }
            Models.User me = Application.Current.Properties["MyProfile"] as Models.User;
            recipe.login = me.login;
            bool result = db.CreateRecipe(recipe);
            if (result)
                DisplayAlert(null, "Рецепт успешно создан", "Ок");
            else
                DisplayAlert("Ошибка", "Не удалось создать рецепт, попробуйте еще раз", "Ок");
        }

        private async Task<MediaFile> PhotoFromDevice()
        {
            string takePhoto = await DisplayActionSheet("Загрузка фото", "Назад", null, new string[] { "Сделать фото", "Выбрать из галереи" });
            await CrossMedia.Current.Initialize();
            if (takePhoto == "Сделать фото")
            {
                if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
                {
                    await DisplayAlert("Не поддерживается", "Ваше устройство не поддерживает данную функцию", "Ок");
                    return null;
                }
                var mediaOptions = new StoreCameraMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                MediaFile selectedImageFile = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

                return selectedImageFile;
            }
            else if (takePhoto == "Выбрать из галереи")
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Не поддерживается", "Ваше устройство не поддерживает данную функцию", "Ок");
                    return null;
                }

                var mediaOptions = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                MediaFile selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                return selectedImageFile;
            }
            else
                return null;
        }

        private async void AddMainPhoto_Clicked(object sender, EventArgs e)
        {
            MediaFile photo = await PhotoFromDevice();
            if (photo != null)
            {
                mainPhotoBytes = File.ReadAllBytes(photo.Path);
                buttonAddPhoto.IsVisible = false;
                labelFoto.IsVisible = false;
                MainPhoto.Source = ImageSource.FromStream(() => photo.GetStream());
                MainPhotoFrame.IsVisible = true;
            }
        }

        private async void StepImageAdd_Clicked(object sender, EventArgs e)
        {
            MediaFile photo = await PhotoFromDevice();
            if (photo != null)
            {
                ImageButton button = sender as ImageButton;
                string id = button.BindingContext as string;
                ImageSource imgSource = ImageSource.FromStream(() => photo.GetStream());
                Steps[int.Parse(id)].image = imgSource;
                imgBytes[int.Parse(id)] = (File.ReadAllBytes(photo.Path));
                (button.Parent as Frame).HeightRequest = 150;
                button.Source = imgSource;
                button.Aspect = Aspect.AspectFill;
            }
        }

        private async void PhotoChange_Clicked(object sender, EventArgs e)
        {
            bool choose = await DisplayAlert("Изменение", "Вы действительно хотите изменить фото?", "Да", "Нет");
            if (choose == true)
                AddMainPhoto_Clicked(this, EventArgs.Empty);
        }

        private void AddStep_Clicked(object sender, EventArgs e)
        {
            Steps.Add(new RecipeStep { StepId = Steps.Count + "", text = (Steps.Count + 1) + "." });
            imgBytes.Add(null);
            CollectionViewRecipeSteps.IsVisible = true;
            CollectionViewRecipeSteps.HeightRequest = 305 * Steps.Count;
            FrameShare.IsVisible = true;
        }

        private void StepperMinus_Clicked(object sender, EventArgs e)
        {
            int value = int.Parse(labelServings.Text);
            if (value > 1)
            {
                value--;
                labelServings.Text = value.ToString();
            }
        }

        private void StepperPlus_Clicked(object sender, EventArgs e)
        {
            int value = int.Parse(labelServings.Text);
            if (value < 100)
            {
                value++;
                labelServings.Text = value.ToString();
            }
        }

        private void PickerIngredients_Changed(object sender, EventArgs e)
        {
            if (EntryIngredientsCount.Text != null)
            {
                Ingredient ingr = new Ingredient();
                Product pr = (sender as Picker).SelectedItem as Product;
                ingr.product = pr;
                ingr.count = int.Parse(EntryIngredientsCount.Text);
                ingr.countType = PickerCountType.SelectedItem.ToString();
                ingredients.Add(ingr);
                EntryIngredientsCount.Text = null;
                CollectionViewIngredients.IsVisible = true;
                CollectionViewIngredients.HeightRequest = 35 * ingredients.Count;
            }
        }

        private void EntryIngredientsCount_Completed(object sender, EventArgs e)
        {
            if (IngredientsPicker.SelectedItem != null)
            {
                Ingredient ingr = new Ingredient();
                Product pr = IngredientsPicker.SelectedItem as Product;
                ingr.product = pr;
                ingr.count = int.Parse(EntryIngredientsCount.Text);
                ingr.countType = PickerCountType.SelectedItem.ToString();
                ingredients.Add(ingr);
                EntryIngredientsCount.Text = null;
                CollectionViewIngredients.IsVisible = true;
                CollectionViewIngredients.HeightRequest = 35 * ingredients.Count;
            }
        }

        private void ButtonShare_Clicked(object sender, EventArgs e)
        {
            if (EntryRecipeName.Text != null)
                recipe.name = EntryRecipeName.Text;
            else
            {
                DisplayAlert("Ошибка", "Имя рецепта не может быть пустым!", "Ок");
                return;
            }

            if (EntryTime.Text != null)
                recipe.time = int.Parse(EntryTime.Text);
            else
            {
                DisplayAlert("Ошибка", "Время приготовления не может быть 0!", "Ок");
                return;
            }

            recipe.servings = int.Parse(labelServings.Text);

            if (ingredients.Count != 0)
                recipe.ingredients = ingredients;
            else
            {
                DisplayAlert("Ошибка", "Отсутствуют ингредиенты в рецепте!", "Ок");
                return;
            }
            recipe.image = MainPhoto.Source;
            recipe.timeType = PickerTimeType.SelectedItem.ToString();

            if (Steps.Count != 0)
                recipe.steps = Steps;
            else
            {
                DisplayAlert("Ошибка", "Отсутствуют этапы приготовления!", "Ок");
                return;
            }
            UploadRecipe();
        }
    }
}

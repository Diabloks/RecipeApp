using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipeApp.Models;
using RecipeApp.Views.ViewRecipe;

namespace RecipeApp.Views.ViewRecipe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewRecipe : ContentPage
    {
        private DataBase db = new DataBase();
        public Recipe recipe { get; set; }

        public ViewRecipe(int RecipeId)
        {
            GetRecipe(RecipeId);
            BindingContext = recipe;
            InitializeComponent();
            Carousel.BindingContext = recipe;
        }

        private async void GetRecipe(int id)
        {
            recipe = db.GetRecipeByID(id);
            if (recipe == null)
            {
                await DisplayAlert("Ошибка", "Не удалось получить данные!", "Ок");
                await Navigation.PopModalAsync();
            }
        }

        private async void BackButton_Click(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void AddToFavourite_Clicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("MyProfile") && Application.Current.Properties["MyProfile"] != null)
            {
                db.AddToFavourite(recipe.id);
            }
        }

        private void RecipeLike_Clicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("MyProfile") && Application.Current.Properties["MyProfile"] != null)
            {
                db.ChangeRateRecipe(recipe.id, 1);
            }
        }

        private void RecipeDis_Clicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("MyProfile") && Application.Current.Properties["MyProfile"] != null)
            {
                db.ChangeRateRecipe(recipe.id, 0);
            }
        }

        private void StepperMinus_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("");
        }
        private void StepperPlus_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("");
        }
    }
}
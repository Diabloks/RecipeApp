using System;
using System.Collections.Generic;

using Xamarin.Forms;
using RecipeApp.Models;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;

namespace RecipeApp.Views.User
{
    public partial class ChangeProfile : ContentPage
    {
        DataBase db = new DataBase();
        Models.User me { get; set; }
        private byte[] mainPhotoBytes { get; set; }
        public ChangeProfile()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!(Application.Current.Properties.ContainsKey("MyProfile") && Application.Current.Properties["MyProfile"] != null))
            {
                Navigation.PopModalAsync();
            }
            else
            {
                me = Application.Current.Properties["MyProfile"] as Models.User;
                BindingContext = me;
                if (me.image == null)
                    ProfilePhoto.Source = ImageSource.FromFile("iconsUser.png");
            }
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            string password = null;
            if (mainPhotoBytes != null && ProfilePhoto.Source != ImageSource.FromFile("iconsUser.png"))
            {
                string url = db.UploadPhoto(mainPhotoBytes);
                if (url != null)
                    me.img_url = url;
            }
            if (newPass.Text != "" && newPassCheck.Text != "" && newPass.Text != null && newPassCheck.Text != null && newPass.Text == newPassCheck.Text)
                password = newPass.Text;
            if(db.UpdateUsersSettings(me, password))
                Navigation.PopModalAsync();
        }

        private async void PhotoChange_Clicked(object sender, EventArgs e)
        {
            MediaFile photo = await PhotoFromDevice();
            if (photo != null)
            {
                mainPhotoBytes = File.ReadAllBytes(photo.Path);
                ProfilePhoto.Source = ImageSource.FromStream(() => photo.GetStream());
            }
            else
            {
                mainPhotoBytes = File.ReadAllBytes(photo.Path);
                ProfilePhoto.Source = ImageSource.FromFile("iconsUser.png");
            }
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
    }
}

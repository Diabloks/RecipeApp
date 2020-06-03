using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipeApp.Models;
using RecipeApp.Views.User;

namespace RecipeApp.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        public Registration()
        {
            InitializeComponent();
        }

        public async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        public async void SignUp_Clicked(object sender, EventArgs e)
        {
            string email = EntryEmail.Text;
            string login = EntryLogin.Text;
            string pass = EntryPassword.Text;
            if (email != null && login != null && pass != null && email != "" && login != "" && pass != "")
            {
                DataBase db = new DataBase();
                if(db.CreateUser(login, pass, email))
                {
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Ошибка", "Не удалось создать аккаунт, возможно имя пользователя уже занято!", "Ок");
                }
            }
            else
            {
                await DisplayAlert("", "Не все поля заполнены!", "Ок");
            }
        }
    }
}
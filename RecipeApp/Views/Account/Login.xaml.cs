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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        public async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        public async void SignIn_Clicked(object sender, EventArgs e)
        {
            string login = EntryLogin.Text;
            string pass = EntryPass.Text;
            if ( login != "" && pass != "" && login != null && pass != null)
            {
                DataBase db = new DataBase();
                if(db.LogIn(login, pass))
                    await Navigation.PopModalAsync();
                else
                    await DisplayAlert("Ошибка", "Не удалось войти в учетную запись, проверьте правильность введеных данных!", "Ок");
            }
            else
            {
                await DisplayAlert("", "Поле не может быть пустым!", "Ок");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipeApp.Views.Account;
using RecipeApp.Views.User;

namespace RecipeApp.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class nonRegis : ContentPage
    {
        public nonRegis()
        {
            if (Application.Current.Properties.ContainsKey("MyProfile") && Application.Current.Properties["MyProfile"] != null)
            {
                Navigation.PushAsync(new ViewProfile(), false);
            }
            else
            {
                InitializeComponent();
            }
        }

        public async void SignIn_Clicked(object sender, EventArgs e)
        {
            Page login = new Login();
            login.Disappearing += (sender2, e2) =>
            {
                if (Application.Current.Properties.ContainsKey("MyProfile") && Application.Current.Properties["MyProfile"] != null)
                    Navigation.PushAsync(new ViewProfile(), false);
            };
            await Navigation.PushModalAsync(login);
        }

        public async void SignUp_Clicked(object sender, EventArgs e)
        {
            Page signup = new Registration();
            signup.Disappearing += (sender2, e2) =>
            {
                if (Application.Current.Properties.ContainsKey("MyProfile") && Application.Current.Properties["MyProfile"] != null)
                    Navigation.PushAsync(new ViewProfile(), false);
            };
            await Navigation.PushModalAsync(signup);
        }
    }
}
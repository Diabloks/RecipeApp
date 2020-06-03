using System;
using System.Collections.Generic;
using Xamarin.Forms;
using RecipeApp.Models;
using RecipeApp.Helpers;
using RecipeApp.Views.Account;

namespace RecipeApp.Views.User
{
    public partial class ViewProfile : ContentPage
    {
        public ViewProfile()
        {
            object obj;
            Application.Current.Properties.TryGetValue("MyProfile", out obj);
            Models.User me = obj as Models.User;
            BindingContext = me;
            InitializeComponent();
        }

        private void MyRecipes_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ViewMyRecipes());
        }

        private void ProfileChange_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ChangeProfile());
        }

        private void ExitProfile_Clicked(object sender, EventArgs e)
        {
            Helpers.Settings.myProfile = string.Empty;
            Helpers.Settings.myPass = string.Empty;
            Application.Current.Properties["MyProfile"] = null;
            Navigation.PushAsync(new nonRegis(), false);
        }
    }
}

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using RecipeApp.Models;

namespace RecipeApp.Views.User
{
    public partial class ViewMyRecipes : ContentPage
    {
        private DataBase db = new DataBase();
        private IList<Recipe> Recipes { get; set; }

        public ViewMyRecipes()
        {       
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!(Application.Current.Properties.ContainsKey("MyProfile") && Application.Current.Properties["MyProfile"] != null))
            {
                await DisplayAlert("", "Ошибка не удалось получить данные учетной записи", "Ок");
                Shell appshell = Application.Current.MainPage as Shell;
                await appshell.GoToAsync("app://RecipeApp.AppShell/profileTab", false);
            }
            else
            {
                Models.User me = Application.Current.Properties["MyProfile"] as Models.User;
                Recipes = db.GetUserRecipes(me.login);
                if (Recipes == null)
                    Recipes = new List<Recipe>();
                favouriteCount.Text = Recipes.Count.ToString() + " рецептов";
                carView.ItemsSource = Recipes;
            }
        }

        private async void viewFavouriteRecipe_Clicked(object sender, EventArgs e)
        {
            Label labelId = (((sender as ImageButton).Parent as Frame).Parent as StackLayout).Children[0] as Label;
            int id;
            if (int.TryParse(labelId.Text, out id))
            {
                await Navigation.PushModalAsync(new ViewRecipe.ViewRecipe(id));
            }
        }
    }
}

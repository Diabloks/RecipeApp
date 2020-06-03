using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipeApp.Models;
using RecipeApp.Views.ViewRecipe;

namespace RecipeApp.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class MainPage : ContentPage
    {
        DataBase db = new DataBase();
        public List<Recipe> AllRecipes { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AllRecipes = db.GetRecipes() as List<Recipe>;
            RecipesCollectionView.ItemsSource = AllRecipes;
        }

        private void SearchCommand(object sender, EventArgs e)
        {
            SearchBar searchBar = sender as SearchBar;
            AllRecipes = db.SearchRecipeName(searchBar.Text) as List<Recipe>;
            RecipesCollectionView.ItemsSource = AllRecipes;
        }

        private void RefreshList(object sender, EventArgs args)
        {
            searchBar.Text = null;
            RefreshView view = sender as RefreshView;
            AllRecipes = db.GetRecipes() as List<Recipe>;
            RecipesCollectionView.ItemsSource = AllRecipes;
            view.IsRefreshing = false;
        }

        private async void SelectionChanged(object sender, EventArgs args)
        {
            Recipe selected = RecipesCollectionView.SelectedItem as Recipe;
            await Navigation.PushModalAsync(new ViewRecipe.ViewRecipe(selected.id));
        }
    }
}
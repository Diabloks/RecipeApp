using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipeApp.Models;
using RecipeApp.Views.Main;

namespace RecipeApp.Views.Main {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  
  public partial class MainPage : ContentPage {
    public List<Recipie> AllRecipies { get; set; } 
    public MainPage() {
      InitializeComponent();
    

    }



    protected override void OnAppearing() {
      base.OnAppearing();

       AllRecipies = new List<Recipie>(Recipies.Get());
      RecipiesCollectionView.ItemsSource = AllRecipies;

    }
     void FilterClicked(object sender, EventArgs args) {
      // MainPage = RecipeApp.Views.Main.MainShell();
    }
  }
}
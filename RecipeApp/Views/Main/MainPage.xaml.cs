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
    public List<Category> AllCategories { get; set; }
    public List<Character> AllCharacters { get; set; }
    public List<TimeModel> AllTimes { get; set; }
    public bool FrameVisibility = false;
    public MainPage() {
      InitializeComponent();
      FrameFilter.IsVisible = FrameVisibility;

    }



    protected override void OnAppearing() {
      base.OnAppearing();

       AllRecipies = new List<Recipie>(Recipies.Get());
      RecipiesCollectionView.ItemsSource = AllRecipies;
      AllTimes = new List<TimeModel>(TimeModels.Get());
      TimeCollectionView.ItemsSource = AllTimes;
      AllCategories = new List<Category>(Categories.Get());
      CategoryCollectionView.ItemsSource = AllCategories;
      AllCharacters = new List<Character>(Characters.Get());
      CharacterCollectionView.ItemsSource = AllCharacters;

    }
     void FilterClicked(object sender, EventArgs args) {
      if (FrameVisibility == false) {
        FrameVisibility = true;
        FilterButton.IsVisible = false;
      }
      else {
        FrameVisibility = false;
        FilterButton.IsVisible = true;
      }
      FrameFilter.IsVisible = FrameVisibility;
    }
  }
}
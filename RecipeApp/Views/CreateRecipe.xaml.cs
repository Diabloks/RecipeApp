using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipeApp.Models;

namespace RecipeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateRecipe : ContentPage
    {
    public List<Ingridient> MainIngridients { get;set; }

    public void Action() {
      MainIngridients.Add(new Ingridient {
        IngridientName = "jjj",
        IngridientValue = "jjj",
        IngridientFormat = ";;;;;"
      }); 
    }

    public bool IsIngredientCollectionActive = false;
        public CreateRecipe()
        {
            InitializeComponent();
      ingridientCollectionView.IsVisible = IsIngredientCollectionActive;
            var monkeyList = new List<string>();
            monkeyList.Add("Яблоко");
            monkeyList.Add("Штук");
            monkeyList.Add("Blue Monkey");
            monkeyList.Add("Squirrel Monkey");
            monkeyList.Add("Golden Lion Tamarin");
            monkeyList.Add("Howler Monkey");
            monkeyList.Add("Japanese Macaque");
            IngridientPicker.ItemsSource = monkeyList;
            FormatPicker.ItemsSource = monkeyList;
        }

        private void AddButton_Clicked(object sender, EventArgs e) {
        ingridientCollectionView.IsVisible = true;
      //  MainIngridients.Clear();
     /*  MainIngridients.Add(new Ingridient {
         IngridientName = "jjj",
         IngridientValue = "jjj",
         IngridientFormat = ";;;;;"
       });
       Ingridient NewIngridient = new Ingridient();
         NewIngridient.IngridientName ="eeeee";
         NewIngridient.IngridientValue = stepperIngridient.Value.ToString();
         NewIngridient.IngridientFormat = FormatPicker.SelectedItem.ToString();
          MainIngridients.Add(NewIngridient);
      try {*/
      // Action();
     /* }
      catch {
        DisplayAlert("Уведомление", "Пришло новое сообщение", "ОK");
      }*/
     
        }
    }
}

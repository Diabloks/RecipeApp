using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeApp.Views.AddReciept {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class AddReciept : ContentView {
    public AddReciept() {
      InitializeComponent();
      var monkeyList = new List<string>();
      monkeyList.Add("Baboon");
      monkeyList.Add("Capuchin Monkey");
      monkeyList.Add("Blue Monkey");
      monkeyList.Add("Squirrel Monkey");
      monkeyList.Add("Golden Lion Tamarin");
      monkeyList.Add("Howler Monkey");
      monkeyList.Add("Japanese Macaque");
      IngridientPicker.ItemsSource = monkeyList;
      FormatPicker.ItemsSource = monkeyList;
    }
  }
}
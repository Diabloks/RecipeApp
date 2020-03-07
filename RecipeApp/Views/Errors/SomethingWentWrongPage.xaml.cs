using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeApp.Views.Errors {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SomethingWentWrongPage : ContentPage {
    public SomethingWentWrongPage() {
      InitializeComponent();
    }
  }
}
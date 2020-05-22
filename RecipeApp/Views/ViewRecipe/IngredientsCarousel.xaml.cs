using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipeApp.Models;

namespace RecipeApp.Views.ViewRecipe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientsCarousel : ContentView
    {
        IList<Ingredient> ingredients { get; set; }
        public IngredientsCarousel()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ingredients = BindingContext as IList<Ingredient>;
            if (ingredients == null)
                ingredients = new List<Ingredient>();
            CollectionViewIngredients.ItemsSource = ingredients;
        }
    }
}
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
    public partial class StepsCarousel : ContentView
    {
        private IList<RecipeStep> steps { get; set; }

        public StepsCarousel()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            steps = BindingContext as IList<RecipeStep>;
            if (steps == null)
                steps = new List<RecipeStep>();
            CollectionViewSteps.ItemsSource = steps;
        }
    }
}
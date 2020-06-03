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
    public class RecipeStepH
    {
        public string StepId { get; set; }
        public string img_url { get; set; }
        public ImageSource image { get; set; }
        public int imgHeight { get; set; }
        public string text { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StepsCarousel : ContentView
    {
        private List<RecipeStepH> steps { get; set; }

        public StepsCarousel()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            int carouselHeight = 0;
            List<RecipeStep> binding = BindingContext as List<RecipeStep>;
            if (binding != null)
            {
                steps = new List<RecipeStepH>();
                for (int i = 0; i < binding.Count; i++)
                {
                    RecipeStepH n = new RecipeStepH();
                    n.StepId = binding[i].StepId;
                    n.img_url = binding[i].img_url;
                    n.image = binding[i].image;
                    n.text = binding[i].text;
                    if (binding[i].image != null)
                    {
                        n.imgHeight = 150;
                        carouselHeight += 300;
                    }
                    else
                    {
                        n.imgHeight = 0;
                        carouselHeight += 150;
                    }
                    steps.Add(n);
                }
                CollectionViewSteps.HeightRequest = carouselHeight;
                CollectionViewSteps.ItemsSource = steps;
            }
        }
    }
}
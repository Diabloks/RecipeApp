using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarouselView.FormsPlugin;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipeApp.Models;
using RecipeApp.Views.ViewRecipe;

namespace RecipeApp.Views.ViewRecipe
{
    public partial class CarouselPage : ContentView
    {
        public CarouselPage()
        {
            InitializeComponent();
            Scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Never;
            Scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Never;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            Recipe recipe = BindingContext as Recipe;
            if (recipe != null)
            {
                List<View> viewList = new List<View>();
                View ingred = new IngredientsCarousel { BindingContext = recipe.ingredients };
                viewList.Add(ingred);
                View steps = new StepsCarousel{ BindingContext = recipe.steps };
                viewList.Add(steps);
                View comments = new CommentsCarousel();
                viewList.Add(comments);
                carViewControl.ItemsSource = viewList;
            }
        }

        private void CarView_SizeChanged(object sender, EventArgs e)
        {

        }

        private void Position_Changed(object sender, TappedEventArgs e)
        {
            int newPos = carViewControl.Position;
            if (newPos == 0)
            {
                Scroll.ScrollToAsync(IngredientsElement, ScrollToPosition.Center, true);
                
                StepsElement.FontAttributes = FontAttributes.None;
                StepsElement.FontSize = 18;

                CommentsElement.FontAttributes = FontAttributes.None;
                CommentsElement.FontSize = 18;

                IngredientsElement.FontAttributes = FontAttributes.Bold;
                IngredientsElement.FontSize = 25;
            }
            else if (newPos == 1)
            {
                Scroll.ScrollToAsync(StepsElement, ScrollToPosition.Center, true);

                IngredientsElement.FontAttributes = FontAttributes.None;
                IngredientsElement.FontSize = 18;

                CommentsElement.FontAttributes = FontAttributes.None;
                CommentsElement.FontSize = 18;

                StepsElement.FontAttributes = FontAttributes.Bold;
                StepsElement.FontSize = 25;
            }
            else
            {
                Scroll.ScrollToAsync(CommentsElement, ScrollToPosition.Center, true);

                IngredientsElement.FontAttributes = FontAttributes.None;
                IngredientsElement.FontSize = 18;

                StepsElement.FontAttributes = FontAttributes.None;
                StepsElement.FontSize = 18;

                CommentsElement.FontAttributes = FontAttributes.Bold;
                CommentsElement.FontSize = 25;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipeApp.Models;
using RecipeApp.Views.ViewRecipe;

namespace RecipeApp.Views.ViewRecipe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewRecipe : ContentPage
    {
        private DataBase db = new DataBase();
        public Recipe recipe { get; set; }

        public ViewRecipe(int RecipeId)
        {
            GetRecipe(RecipeId);
            BindingContext = recipe;
            InitializeComponent();
            Carousel.BindingContext = recipe;
        }

        private async void GetRecipe(int id)
        {
            recipe = db.GetRecipeByID(id);
            if (recipe == null)
            {
                await DisplayAlert("Ошибка", "Не удалось получить данные!", "Ок");
                await Navigation.PopModalAsync();
            }
        }

        private void Carousel_SizeChanged(object sender, EventArgs e)
        {
        }

        private async void BackButton_Click(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void Serving_Changed(object sender, EventArgs e)
        {

        }

        private void StepperMinus_Clicked(object sender, EventArgs e)
        {

        }
        private void StepperPlus_Clicked(object sender, EventArgs e)
        {

        }
    }
}
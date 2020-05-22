using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RecipeApp.Models
{
    public class Product
    {
        public string id { get; set; }
        public string name { get; set; }
        public string proteins { get; set; }
        public string fats { get; set; }
        public string carbohydrates { get; set; }
        public string calories { get; set; }
        public string img_url { get; set; }
        public ImageSource image { get; set; }
    }
}

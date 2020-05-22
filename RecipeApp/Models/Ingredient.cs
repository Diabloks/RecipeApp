using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp.Models
{
    public class Ingredient
    {
        public Product product { get; set; }
        public double count { get; set; }
        public string countType { get; set; }
    }
}

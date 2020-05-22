using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RecipeApp.Models
{
    public class RecipeStep
    {
        public string StepId { get; set; }
        public string img_url { get; set; }
        public ImageSource image { get; set; }
        public string text { get; set; }
    }
}

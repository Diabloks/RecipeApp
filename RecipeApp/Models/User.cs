using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RecipeApp.Models
{
    public class User
    {
        public string login { get; set; }
        public string email { get; set; }
        public DateTime date_of_registration { get; set; }
        public string img_url { get; set; }
        public ImageSource image { get; set; }
        //public Recipe favorite { get; set; }
        //public int followers { get; set; }
        //public int follow { get; set; }
    }
}

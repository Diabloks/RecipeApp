using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RecipeApp.Models
{
    public class Comment
    {
        private int id { get; set; }
        public int parent { get; set; }
        public string login { get; set; }
        public string img_url { get; set; }
        public string text { get; set; }
        public int rating { get; set; }
        public DateTime date { get; set; }
        public ImageSource img { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RecipeApp.Models
{
    public class Comment
    {
        public int id { get; set; } //Id комментария в бд
        public string parent { get; set; }
        public string login { get; set; }
        public string img_url { get; set; }
        public string text { get; set; }
        public int rating { get; set; }
        public DateTime date { get; set; }
        public ImageSource img { get; set; }
        public List<Comment> childComments { get; set; }
    }
}

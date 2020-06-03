using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipeApp.Models;
using System.Collections.ObjectModel;

namespace RecipeApp.Views.ViewRecipe
{
    public class CommentCarousel
    {
        public string visualId { get; set; }
        public int id { get; set; }
        public string parent { get; set; }
        public string login { get; set; }
        public string img_url { get; set; }
        public string text { get; set; }
        public int rating { get; set; }
        public DateTime date { get; set; }
        public ImageSource img { get; set; }
        public int childCount { get; set; }
        public ObservableCollection<CommentCarousel> childComments { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentsCarousel : ContentView
    {
        private DataBase db = new DataBase();
        private Recipe recipe { get; set; }
        public ObservableCollection<CommentCarousel> list { get; set; }

        public CommentsCarousel()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            recipe = BindingContext as Recipe;
            list = new ObservableCollection<CommentCarousel>();
            if (recipe != null && recipe.comments != null)
            {
                for(int i= recipe.comments.Count-1; i >= 0; i--)
                {
                    CommentCarousel comCar = new CommentCarousel();
                    comCar.visualId = i.ToString();
                    comCar.id = recipe.comments[i].id;
                    comCar.parent = recipe.comments[i].parent;
                    comCar.login = recipe.comments[i].login;
                    comCar.rating = recipe.comments[i].rating;
                    comCar.text = recipe.comments[i].text;
                    comCar.img_url = recipe.comments[i].img_url;
                    if(comCar.img_url == null)
                    {
                        comCar.img = ImageSource.FromFile("iconsUserSelected.png");
                    }
                    else
                    {
                        try
                        {
                            comCar.img = ImageSource.FromUri(new Uri(comCar.img_url));
                        }
                        catch(Exception)
                        {
                            comCar.img = ImageSource.FromFile("iconsUserSelected.png");
                        }
                    }
                    comCar.childComments = new ObservableCollection<CommentCarousel>();
                    if (recipe.comments[i].childComments != null)
                    {
                        for (int j = 0; j < recipe.comments[i].childComments.Count; j++)
                        {
                            CommentCarousel comCarChild = new CommentCarousel();
                            comCarChild.visualId = comCar.visualId + "/" + j.ToString();
                            comCarChild.id = recipe.comments[i].childComments[j].id;
                            comCarChild.parent = recipe.comments[i].childComments[j].parent;
                            comCarChild.login = recipe.comments[i].childComments[j].login;
                            comCarChild.rating = recipe.comments[i].childComments[j].rating;
                            comCarChild.text = recipe.comments[i].childComments[j].text;
                            comCarChild.img_url = recipe.comments[i].childComments[j].img_url;
                            if (comCarChild.img_url == null)
                            {
                                comCarChild.img = ImageSource.FromFile("iconsUserSelected.png");
                            }
                            else
                            {
                                try
                                {
                                    comCarChild.img = ImageSource.FromUri(new Uri(comCarChild.img_url));
                                }
                                catch (Exception)
                                {
                                    comCarChild.img = ImageSource.FromFile("iconsUserSelected.png");
                                }
                            }
                            comCar.childComments.Add(comCarChild);
                        }
                    }
                    comCar.childCount = comCar.childComments.Count;
                    list.Add(comCar);
                }
                CollectionViewComments.ItemsSource = list;
            }
        }

        private void showChildComments_Clicked(object sender, EventArgs e)
        {
            Frame childCommentsFrame = ((((sender as ImageButton).Parent as StackLayout).Parent as Frame).Parent as Grid).Children[5] as Frame;
            if (childCommentsFrame.IsVisible)
                childCommentsFrame.IsVisible = false;
            else
                childCommentsFrame.IsVisible = true;
        }

        private void SendNewComment_Clicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("MyProfile") && Application.Current.Properties["MyProfile"] != null)
            {
                if (myComment.Text != null && myComment.Text != "")
                {
                    Models.User me = Application.Current.Properties["MyProfile"] as Models.User;
                    Comment created = new Comment();
                    created.text = myComment.Text;
                    created.login = me.login;

                    string idStr = db.CreateComment(recipe.id, created);
                    if (idStr != null)
                    {
                        CommentCarousel comCar = new CommentCarousel();
                        int id;
                        if (int.TryParse(idStr, out id))
                        {
                            comCar.visualId = list.Count.ToString();
                            comCar.id = id;
                            comCar.text = created.text;
                            comCar.login = created.login;
                            if (me.img_url != null)
                            {
                                try
                                {
                                    comCar.img = ImageSource.FromUri(new Uri(me.img_url));
                                }
                                catch (Exception)
                                {
                                    comCar.img = ImageSource.FromFile("iconsUserSelected.png");
                                }
                            }
                            else
                            {
                                comCar.img = ImageSource.FromFile("iconsUserSelected.png");
                            }
                            list.Insert(0, comCar);
                            try
                            {
                                (sender as Entry).Text = null;
                            }
                            catch(Exception )
                            {

                            }
                        }
                    }
                }
            }
        }

        private void commentLike_Clicked(object sender, EventArgs e)
        {
            ImageButton button = sender as ImageButton;
            string commentId = button.BindingContext.ToString();
            int id;
            if(int.TryParse(commentId, out id))
            {
                if(db.ChangeRateComment(id, 1))
                {
                    Label rate = (button.Parent as StackLayout).Children[1] as Label;
                    int value;
                    if(int.TryParse(rate.Text, out value))
                    {
                        value++;
                        rate.Text = value.ToString();
                    }
                }
            }

        }

        private void EntryChild_Completed(object sender, EventArgs e)
        {
            Entry send = sender as Entry;
            string text = send.Text;
            send.Text = null;
            if (text != null && text != "")
            {
                string visualId = send.BindingContext as string;
                if(CreateComment(text, visualId))
                {
                    Label childrenCommentsCount = ((((((send.Parent as StackLayout).Parent as StackLayout).Parent as Frame).Parent as Grid).Children[4] as Frame).Content as StackLayout).Children[1] as Label;
                    int value;
                    if (int.TryParse(childrenCommentsCount.Text, out value))
                    {
                        value++;
                        childrenCommentsCount.Text = value.ToString();
                    }
                }
            }
        }

        private void childCommentCreate_Clicked(object sender, EventArgs e)
        {
            Entry send = ((sender as ImageButton).Parent as StackLayout).Children[0] as Entry;
            string text = send.Text;
            send.Text = null;
            if (text != null && text != "")
            {
                string visualId = send.BindingContext as string;
                if(CreateComment(text, visualId))
                {
                   Label childrenCommentsCount = ((((((send.Parent as StackLayout).Parent as StackLayout).Parent as Frame).Parent as Grid).Children[4] as Frame).Content as StackLayout).Children[1] as Label;
                   int value;
                   if (int.TryParse(childrenCommentsCount.Text, out value))
                   {
                       value++;
                        childrenCommentsCount.Text = value.ToString();
                   }
                }
            }
        }

        private bool CreateComment(string text, string parent)
        {
            if (Application.Current.Properties.ContainsKey("MyProfile") && Application.Current.Properties["MyProfile"] != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].visualId == parent)
                    {
                        Models.User me = Application.Current.Properties["MyProfile"] as Models.User;
                        Comment created = new Comment();
                        created.text = text;
                        created.login = me.login;
                        created.parent = list[i].id.ToString();

                        string idStr = db.CreateComment(recipe.id, created);
                        if (idStr != null)
                        {
                            CommentCarousel comCar = new CommentCarousel();
                            int id;
                            if (int.TryParse(idStr, out id))
                            {
                                comCar.id = id;
                                comCar.text = created.text;
                                comCar.login = created.login;
                                comCar.parent = created.parent;
                                if (me.img_url != null)
                                {
                                    try
                                    {
                                        comCar.img = ImageSource.FromUri(new Uri(me.img_url));
                                    }
                                    catch (Exception)
                                    {
                                        comCar.img = ImageSource.FromFile("iconsUserSelected.png");
                                    }
                                }
                                else
                                {
                                    comCar.img = ImageSource.FromFile("iconsUserSelected.png");
                                }
                                if (list[i].childComments == null)
                                    list[i].childComments = new ObservableCollection<CommentCarousel>();
                                list[i].childComments.Add(comCar);
                                list[i].childCount = list[i].childComments.Count;
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            else
                return false;
        }
    }
}
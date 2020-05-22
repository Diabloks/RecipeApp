using Android.Content;
using RecipeApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer_Droid))]
namespace RecipeApp.Droid.Renderers
{
    public class CustomEntryRenderer_Droid : EntryRenderer
    {
        public CustomEntryRenderer_Droid(Context context) : base(context)
        {

        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}
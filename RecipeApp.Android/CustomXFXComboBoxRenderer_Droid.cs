using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Android.Content;
using Android.Content.Res;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xfx;
using Xfx.Controls.Droid.Forms.Internals;
using Xfx.Controls.Droid.Renderers;
using Xfx.Controls.Droid.XfxComboBox;
using Resource = Android.Resource;

[assembly: ExportRenderer(typeof(XfxComboBox), typeof(CustomXFXComboBoxRenderer_Droid))]
namespace Xfx.Controls.Droid.Renderers
{
    [Android.Runtime.Preserve(AllMembers = true)]
    class CustomXFXComboBoxRenderer_Droid : XfxComboBoxRendererDroid
    {
        public CustomXFXComboBoxRenderer_Droid(Context context) : base(context)
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            Control?.EditText.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}
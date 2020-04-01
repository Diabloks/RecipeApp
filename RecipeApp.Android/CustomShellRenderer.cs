using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using RecipeApp;
using RecipeApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AppShell), typeof(MyShellRenderer))]
namespace RecipeApp.Droid
{
    public class MyShellRenderer : ShellRenderer
    {
        public MyShellRenderer(Context context) : base(context)
        {
        }

        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        { 
            return new CustomBottomNavAppearance();
        }
    }

    public class CustomBottomNavAppearance : IShellBottomNavViewAppearanceTracker
    {
        public void Dispose()
        {

        }

        public void ResetAppearance(BottomNavigationView bottomView)
        {

        }

        public void SetAppearance(BottomNavigationView bottomView, ShellAppearance appearance)
        {
            IMenu myMenu = bottomView.Menu;

            IMenuItem myItemOne = myMenu.GetItem(0);

            if (myItemOne.IsChecked)
            {
             //   myItemOne.SetIcon(Resource.Drawable.iconsMainSelected); //Когда иконка в активном состоянии
            }
            else
            {
             //   myItemOne.SetIcon(Resource.Drawable.iconsMain); //Когда иконка в не активном состоянии
            }

            //Таже логика если есть второй, третий элемент в tabbar

        }
    }
}
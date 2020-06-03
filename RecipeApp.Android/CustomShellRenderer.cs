using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.BottomNavigation;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using RecipeApp;
using RecipeApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;
using Android.Util;

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
            CustomBottomNavAppearance customBottomNav = new CustomBottomNavAppearance();
            return customBottomNav;
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

        public void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            bottomView.ItemIconSize = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 35, bottomView.Context.Resources.DisplayMetrics);
            bottomView.SetShiftMode(false, false);
            bottomView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityUnlabeled;

            IMenu myMenu = bottomView.Menu;

            IMenuItem myItemOne = myMenu.GetItem(0);
            IMenuItem myItemTwo = myMenu.GetItem(1);
            IMenuItem myItemThree = myMenu.GetItem(2);
            IMenuItem myItemFour = myMenu.GetItem(3);

            if (myItemOne.IsChecked)
                myItemOne.SetIcon(Resource.Drawable.iconsMainSelected); //Когда иконка в активном состоянии
            else
                myItemOne.SetIcon(Resource.Drawable.iconsMain); //Когда иконка в не активном состоянии

            if (myItemTwo.IsChecked)
                myItemTwo.SetIcon(Resource.Drawable.iconsRecipeSelected); //Когда иконка в активном состоянии
            else
                myItemTwo.SetIcon(Resource.Drawable.iconsRecipe); //Когда иконка в не активном состоянии

            if (myItemThree.IsChecked)
                myItemThree.SetIcon(Resource.Drawable.iconsFavoriteSelected); //Когда иконка в активном состоянии
            else
                myItemThree.SetIcon(Resource.Drawable.iconsFavorite); //Когда иконка в не активном состоянии

            if (myItemFour.IsChecked)
                myItemFour.SetIcon(Resource.Drawable.iconsUserSelected); //Когда иконка в активном состоянии
            else
                myItemFour.SetIcon(Resource.Drawable.iconsUser); //Когда иконка в не активном состоянии

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecipeApp;
using RecipeApp.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(AppShell), typeof(MyShellRenderer))]
namespace RecipeApp.iOS
{
    public class MyShellRenderer : ShellRenderer
    {
        protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
        {
            var renderer = base.CreateShellSectionRenderer(shellSection);
            if (renderer != null)
            {
                (renderer as ShellSectionRenderer).NavigationBar.SetBackgroundImage(UIImage.FromFile("monkey.png"), UIBarMetrics.Default);
            }
            return renderer;
        }

        protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
        {
            return new CustomTabbarAppearance();
        }
    }

    public class CustomTabbarAppearance : IShellTabBarAppearanceTracker
    {
        public void Dispose()
        {

        }

        public void ResetAppearance(UITabBarController controller)
        {

        }

        public void SetAppearance(UITabBarController controller, ShellAppearance appearance)
        {
            UITabBar myTabBar = controller.TabBar;
            myTabBar.TintColor = UIColor.Black;
            myTabBar.UnselectedItemTintColor = UIColor.Black;

            if (myTabBar.Items != null)
            {
                UITabBarItem itemHome = myTabBar.Items[0];
                itemHome.Image = UIImage.FromBundle("iconsMain.png");
                itemHome.SelectedImage = UIImage.FromBundle("iconsMainSelected.png");


                UITabBarItem itemSearch = myTabBar.Items[1];
                itemSearch.Image = UIImage.FromBundle("iconsTomato.png");
                itemSearch.SelectedImage = UIImage.FromBundle("iconsTomatoSelected.png");

                UITabBarItem itemAdd = myTabBar.Items[2];
                itemAdd.Image = UIImage.FromBundle("iconsRecipe.png");
                itemAdd.SelectedImage = UIImage.FromBundle("iconsRecipeSelected.png");

                UITabBarItem itemFavorite = myTabBar.Items[3];
                itemFavorite.Image = UIImage.FromBundle("iconsLike.png");
                itemFavorite.SelectedImage = UIImage.FromBundle("iconsLikeSelected.png");

                UITabBarItem itemProfile = myTabBar.Items[4];
                itemProfile.Image = UIImage.FromBundle("iconsUser.png");
                itemProfile.SelectedImage = UIImage.FromBundle("iconsUserSelected.png");

            }

        }

        public void UpdateLayout(UITabBarController controller)
        {

            UITabBar myTabBar = controller.TabBar;

            foreach (var barItem in myTabBar.Items)
            {
                barItem.ImageInsets = new UIEdgeInsets(0, 0, 0, 0);
                //barItem.TitlePositionAdjustment = new UIOffset(10, 0);
            }
        }
    }
}

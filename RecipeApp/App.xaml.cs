using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipeApp.Models;
using Newtonsoft.Json;
using RecipeApp.Helpers;

namespace RecipeApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            //Settings.GeneralSettings = string.Empty;
            //Settings.Products = string.Empty;
            //Settings.myProfile = string.Empty;
            //Settings.myPass = string.Empty;
            DataBase db = new DataBase();
            db.SyncProducts();

            if (Settings.myProfile != string.Empty && Settings.myPass != string.Empty)
            {
                try
                {
                    User me = JsonConvert.DeserializeObject<User>(Settings.myProfile);
                    if(!db.LogIn(me.login, Settings.myPass))
                    {
                        Settings.myProfile = string.Empty;
                        Settings.myPass = string.Empty;
                    }
                }
                catch(Exception)
                {
                    Settings.myProfile = string.Empty;
                    Settings.myPass = string.Empty;
                }
            }
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipeApp.Models;

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
            DataBase db = new DataBase();
            db.SyncProducts();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

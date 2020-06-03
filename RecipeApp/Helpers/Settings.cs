using System;
using System.Collections.Generic;
using System.Text;

using RecipeApp.Models;

using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace RecipeApp.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const string ProductsKey = "products_key";
        private static readonly string ProductsDefault = string.Empty;

        private const string myProfileKey = "myProfile_key";
        private static readonly string myProfileDefault = string.Empty;

        private const string myPassKey = "myPass_key";
        private static readonly string myPassDefault = string.Empty;

        #endregion


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        public static string Products
        {
            get
            {
                return AppSettings.GetValueOrDefault(ProductsKey, ProductsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(ProductsKey, value);
            }
        }

        public static string myProfile
        {
            get
            {
                return AppSettings.GetValueOrDefault(myProfileKey, myProfileDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(myProfileKey, value);
            }
        }

        public static string myPass
        {
            get
            {
                return AppSettings.GetValueOrDefault(myPassKey, myPassDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(myPassKey, value);
            }
        }
    }
}

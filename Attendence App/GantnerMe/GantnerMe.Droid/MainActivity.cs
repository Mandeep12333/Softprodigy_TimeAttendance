using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Transformations;
using FFImageLoading;
using Xamarin.Forms;
using Plugin.SecureStorage;
using Plugin.Permissions;

namespace GantnerMe.Droid
{
    [Activity(Label = "GantnerMe", Icon = "@drawable/appicon", 
        MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustPan)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var thai = new System.Globalization.UmAlQuraCalendar();
            SecureStorageImplementation.StoragePassword = "Your Password";
            FFImageLoading.Forms.Droid.CachedImageRenderer.Init();
            var ignore = new CircleTransformation();
            var config = new FFImageLoading.Config.Configuration()
            {
                VerboseLogging = false,
                VerbosePerformanceLogging = false,
                VerboseMemoryCacheLogging = false,
                VerboseLoadingCancelledLogging = false,
                Logger = new CustomLogger(),
            };
            Xamarin.FormsMaps.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageService.Instance.Initialize(config);
            App.ScreenSize = new Xamarin.Forms.Size((int)(Resources.DisplayMetrics.WidthPixels /
                Resources.DisplayMetrics.Density), (int)(Resources.DisplayMetrics.HeightPixels / 
                Resources.DisplayMetrics.Density));
           // App.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);
           // App.ScreenHeight = (int)Resources.DisplayMetrics.HeightPixels;
            Forms.ViewInitialized += (sender, e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.View.StyleId))
                {
                    e.NativeView.ContentDescription = e.View.StyleId;
                }
            };
            LoadApplication(new App());
        }

        public override void OnBackPressed()
        {
            string Pagename = App.CheckPageName();
            if (Pagename == "GantnerMe.MainPage")
            {
                if (Device.OS == TargetPlatform.Android)
                {
                     App.OnBackButtonPressed();
                    //base.OnBackPressed();
                }
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public class CustomLogger : FFImageLoading.Helpers.IMiniLogger
        {
            public void Debug(string message)
            {
                Console.WriteLine(message);
            }

            public void Error(string errorMessage)
            {
                Console.WriteLine(errorMessage);
            }

            public void Error(string errorMessage, Exception ex)
            {
                Error(errorMessage + System.Environment.NewLine + ex.ToString());
            }
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


    }
}


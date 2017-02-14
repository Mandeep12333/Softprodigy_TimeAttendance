using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using FFImageLoading;
using FFImageLoading.Transformations;
using CoreGraphics;
using Syncfusion.SfDataGrid.XForms.iOS;
using System.Threading;

namespace GantnerMe.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        UIWindow window;
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            //UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(53, 213, 214); //bar background
            //UINavigationBar.Appearance.TintColor = UIColor.White; //Tint color of button items
            //UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
            //{
            //    Font = UIFont.FromName("HelveticaNeue-Light", (nfloat)20f),
            //    TextColor = UIColor.White
            //});

            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(59, 115, 150); //bar background
            UINavigationBar.Appearance.TintColor = UIColor.White; //Tint color of button items
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
            {
                Font = UIFont.FromName("HelveticaNeue-Light", (nfloat)18f),
                TextColor = UIColor.White
            });
            var dictionary = NSDictionary.FromObjectsAndKeys(new[] { "Mozilla/5.0 (iPhone; CPU iPhone OS 7_1 like Mac OS X) AppleWebKit/537.51.2 (KHTML, like Gecko) Version/7.0 Mobile/11D167 Safari/9537.53" }, new[] { "UserAgent" });
            NSUserDefaults.StandardUserDefaults.RegisterDefaults(dictionary);
            var indexVC = new UIViewController();
            window = new UIWindow((CGRect)UIScreen.MainScreen.Bounds);
            window.RootViewController = indexVC;
            window.MakeKeyAndVisible();
            var thai = new System.Globalization.UmAlQuraCalendar();
            Xamarin.FormsMaps.Init();
            global::Xamarin.Forms.Forms.Init();
            App.ScreenSize = new Xamarin.Forms.Size(UIScreen.MainScreen.Bounds.Width,
                UIScreen.MainScreen.Bounds.Height);
            // App.ScreenWidth = (int)UIScreen.MainScreen.Bounds.Width;
            // App.ScreenHeight = (int)UIScreen.MainScreen.Bounds.Height;
            SfDataGridRenderer.Init();
            var config = new FFImageLoading.Config.Configuration()
            {
                VerboseLogging = false,
                VerbosePerformanceLogging = false,
                VerboseMemoryCacheLogging = false,
                VerboseLoadingCancelledLogging = false,
                Logger = new CustomLogger(),
            };
            ImageService.Instance.Initialize(config);
            LoadApplication(new App());
            #region Localization debug info
            foreach (var s in NSLocale.PreferredLanguages)
            {
                Console.WriteLine("pref:" + s);
            }

            var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
            var iosLanguageAuto = NSLocale.AutoUpdatingCurrentLocale.LanguageCode;
            Console.WriteLine("nslocaleid:" + iosLocaleAuto);
            Console.WriteLine("nslanguage:" + iosLanguageAuto);


            var iosLocale = NSLocale.CurrentLocale.LocaleIdentifier;
            var iosLanguage = NSLocale.CurrentLocale.LanguageCode;
            var netLocale = iosLocale.Replace("_", "-");
            var netLanguage = iosLanguage.Replace("_", "-");
            Console.WriteLine("ios:" + iosLanguage + " " + iosLocale);
            Console.WriteLine("net:" + netLanguage + " " + netLocale);

            Console.WriteLine("culture:" + Thread.CurrentThread.CurrentCulture);
            Console.WriteLine("uiculture:" + Thread.CurrentThread.CurrentUICulture);
            #endregion
            FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
            var ignore = new CircleTransformation();
            return base.FinishedLaunching(app, options);
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
    }
}

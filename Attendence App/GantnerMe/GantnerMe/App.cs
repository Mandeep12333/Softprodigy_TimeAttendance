using GantnerMe.Class;
using GantnerMe.Interface;
using GantnerMe.Resx;
using GantnerMe.ViewModel;
using ModernHttpClient;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GantnerMe
{
    public class App : Application
    {
        public class MultipleCircle
        {
            public double Lat { get; set; }
            public double Long { get; set; }
        }
        public class Location1
        {
            public double lat { get; set; }
            public double lng { get; set; }
            public double radius { get; set; }
        }
        public UserDb DB = new UserDb();
        public TimerSettingDB timerDB = new TimerSettingDB();
        public Common commonobj = new Common();
        public static Location LocationList = new Location();
        public static List<GetAllReasons> GetReasonlist = new List<GetAllReasons>();
        public static string AppName { get { return "GantbnerMe"; } }
        public static Size ScreenSize { get; set; }
        static public int ScreenWidth;
        static public int ScreenHeight;
        public static INavigation Navigation { get; private set; }
        public App()
        {
            if (Device.OS == TargetPlatform.Android)
            {
                MainPage = new NavigationPage(new StartPage())
                {
                    Title = "Center",
                    BarTextColor = Color.FromHex("#FFFFFF"),
                    BarBackgroundColor = Color.FromHex("#2F6686")
                };

                // Start Activity Screen//

                //var GetuserToken = DB.GetUser().ToList();
                //if (GetuserToken.Count > 0)
                //{
                //    var Langcode = CrossSecureStorage.Current.GetValue("Langcode");
                //    if (Langcode == "en")
                //    {
                //        GlobalLanguageCulture.LanguageCode = "en";
                //        GlobalLanguageCulture.SelectedLang = "English";
                //    }
                //    else
                //    {
                //        GlobalLanguageCulture.LanguageCode = "ar";
                //        GlobalLanguageCulture.SelectedLang = "العربية";

                //    }
                //    GlobalUserDetail.GlobalGUID = GetuserToken[0].UserToken;
                //    //    MainPage = new NavigationPage(new SplashScreenPage())
                //    //    { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };

                //    //    //MainPage = new NavigationPage(new LoginPage("", "", "Used"))
                //    //    //{ Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };
                //    App.Current.MainPage = new MasterMainPage { Detail = new NavigationPage(new MainPage()) { Title = "Center", BarBackgroundColor = Color.FromHex("#2F6686"), BarTextColor = Color.FromHex("#FFFFFF") } };

                //}
                //else
                //{
                //    MainPage = new NavigationPage(new StartPage())
                //    { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };

                //    var timervalues = timerDB.GetTimerSetting();
                //    if (timervalues == null)
                //        commonobj.addtimervalue();
                //}

            }
            if (Device.OS == TargetPlatform.iOS)
            {
                MainPage = new NavigationPage(new StartPage())
                { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };

                // IOS//
                //var GetuserToken = DB.GetUser().ToList();
                //if (GetuserToken.Count > 0)
                //{
                //    var Langcode = CrossSecureStorage.Current.GetValue("Langcode");
                //    if (Langcode == "en")
                //    {
                //        GlobalLanguageCulture.LanguageCode = "en";
                //        GlobalLanguageCulture.SelectedLang = "English";
                //    }
                //    else
                //    {
                //        GlobalLanguageCulture.LanguageCode = "ar";
                //        GlobalLanguageCulture.SelectedLang = "العربية";

                //    }
                //    GlobalUserDetail.GlobalGUID = GetuserToken[0].UserToken;
                //    App.Current.MainPage = new MasterMainPage { Detail = new NavigationPage(new MainPage()) { Title = "Center", BarBackgroundColor = Color.FromHex("#2F6686"), BarTextColor = Color.FromHex("#FFFFFF") } };
                //    //MainPage = new NavigationPage(new LoginPage("", "", "Used"))
                //    //{ Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };
                //    // App.Current.MainPage = new MasterMainPage { Detail = new NavigationPage(new MainPage()) { Title = "Center", BarBackgroundColor = Color.FromHex("#2F6686"), BarTextColor = Color.FromHex("#FFFFFF") } };

                //}
                //else
                //{
                //    MainPage = new NavigationPage(new StartPage())
                //    { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };

                //    var timervalues = timerDB.GetTimerSetting();
                //    if (timervalues == null)
                //        commonobj.addtimervalue();
                //}
            }
            // The root page of your application
        }

        public void CheckNetwork()
        {
            bool Status = CrossConnectivity.Current.IsConnected; //? "Connected" : "No Connection";
            if (Status)
            {
                // Connetec//
            }
            else
            {

            }
        }

        public static string CheckPageName()
        {
            string pagename = string.Empty;
            NavigationPage mainPage = App.Current.MainPage as NavigationPage;
            if (mainPage != null)
            {
                pagename = "";

            }
            else
            {
                NavigationPage NP = ((NavigationPage)((MasterDetailPage)App.Current.MainPage).Detail);
                string CurrentPage = NP.CurrentPage.ToString();
                pagename = CurrentPage;
            }
            return pagename;

        }

        public static async void OnBackButtonPressed()
        {

            var result = await App.Current.MainPage.DisplayAlert("Exit", "Do you really want to exit?", "Yes", "No");
            if (result)
            {
                GlobalUserDetail.RatePresentation = null;
                GlobalUserDetail.EventFeedbackMessage = null;
                GlobalUserDetail.LoginSuccessMessage = null;
                GlobalUserDetail.RegisterSuccess = null;
                DependencyService.Get<IAndroidMethods>().CloseApp();
            }
            else
            {

            }
        }

        public static async Task Sleep(int ms)
        {
            await Task.Delay(ms);
        }
        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

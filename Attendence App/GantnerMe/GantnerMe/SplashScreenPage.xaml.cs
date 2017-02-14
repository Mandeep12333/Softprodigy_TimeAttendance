using GantnerMe.Class;
using GantnerMe.Interface;
using GantnerMe.ViewModel;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GantnerMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreenPage : ContentPage
    {
        public UserDb DB = new UserDb();
        public Common commonobj = new Common();
        public TimerSettingDB timerDB = new TimerSettingDB();
        public SplashScreenPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //        WebView webview = new WebView();
            //        // load html
            //        var html = new HtmlWebViewSource();
            //        html.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
            //        html.Html = @"<html><head>    
            //</head>
            //<body>

            //<img height='730' width='400' src='splash_screen.gif'/>
            //</body>
            //</html>";
            //        webviewsplash.Source = html;

            WebView webview = new WebView();
            // load html
            var html = new HtmlWebViewSource();
            html.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
            html.Html = @"<html><head>    
    </head>
    <body>
    <script type='text/javascript'></script>
    <img height='700' width='380' src='splash_screen.gif'/>
    </body>
    </html>";
            webviewsplash.Source = html;

        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await App.Sleep(3000);
            RedirectToStartPage();

        }

        public void RedirectToMainPage()
        {
            var GetuserToken = DB.GetUser().ToList();
            if (GetuserToken.Count > 0)
            {
                var Langcode = CrossSecureStorage.Current.GetValue("Langcode");
                if (Langcode == "en")
                {
                    GlobalLanguageCulture.LanguageCode = "en";
                    GlobalLanguageCulture.SelectedLang = "English";
                }
                else
                {
                    GlobalLanguageCulture.LanguageCode = "ar";
                    GlobalLanguageCulture.SelectedLang = "العربية";

                }
                GlobalUserDetail.GlobalGUID = GetuserToken[0].UserToken;
                App.Current.MainPage = new MasterMainPage { Detail = new NavigationPage(new MainPage())
                { Title = "Center", BarBackgroundColor = Color.FromHex("#2F6686"),
                    BarTextColor = Color.FromHex("#FFFFFF") } };

                //MainPage = new NavigationPage(new LoginPage("", "", "Used"))
                //{ Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };
                // App.Current.MainPage = new MasterMainPage { Detail = new NavigationPage(new MainPage()) { Title = "Center", BarBackgroundColor = Color.FromHex("#2F6686"), BarTextColor = Color.FromHex("#FFFFFF") } };

            }

            else
            {
                App.Current.MainPage = new NavigationPage(new StartPage())
                { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };

                var timervalues = timerDB.GetTimerSetting();
                if (timervalues == null)
                    commonobj.addtimervalue();
            }
        }

        public void RedirectToStartPage()
        {
            App.Current.MainPage = new NavigationPage(new StartPage())
            { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }
    }
}

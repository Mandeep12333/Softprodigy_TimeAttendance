using GantnerMe.Class;
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
    public partial class StartPage : ContentPage
    {
        public UserDb DB = new UserDb();
        public Common commonobj = new Common();
        public TimerSettingDB timerDB = new TimerSettingDB();
        string cachedUser = string.Empty;
        public int _tapCount = 0;
        public StartPage()
        {
            InitializeComponent();
           // SetupGestureResponse();
            gestureView.SwipeRight += GestureView_SwipeRight;
            gestureView.SwipeLeft += GestureView_SwipeLeft;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private  void GestureView_SwipeLeft(object sender, EventArgs e)
        {
            gestureView.Opacity= .5;
            gestureView.Opacity = 1;
            RedirectToMainPage();
        }

        private async void GestureView_SwipeRight(object sender, EventArgs e)
        {
           // await DisplayAlert("", "Right", "OK");
        }

        private void SetupGestureResponse()
        {
            gestureView.SwipeDown += (sender, e) =>
            {
                DisplayAlert("", "Down", "OK");
            };
            gestureView.SwipeLeft += (sender, e) =>
            {
                DisplayAlert("", "Left", "OK");
            };
            gestureView.SwipeRight += (sender, e) =>
            {
                DisplayAlert("", "Right", "OK");
            };
            gestureView.SwipeUp += (sender, e) =>
            {
                DisplayAlert("", "Up", "OK");
            };
        }

        protected override void OnAppearing()
        {
            _tapCount = 0;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

        }
        public async void OnTapGestNextClick(object sender, EventArgs args)
        {
            imgnext.Opacity = .5;
            await Task.Delay(100);
            imgnext.Opacity = 1;
            _tapCount = _tapCount + 1;
            if (_tapCount > 1)
            {
                // _tapCount = 0;
                return;
            }
            else
            {

                RedirectToMainPage();

            }

        }

        public void RedirectToMainPage()
        {
            var GetuserToken = DB.GetUser().ToList();
            if (GetuserToken.Count > 0)
            {
                var ServerUrl= CrossSecureStorage.Current.GetValue("Url");
                GlobalUserDetail.ServerurlLink = ServerUrl;
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
                App.Current.MainPage = new MasterMainPage { Detail = new NavigationPage(new MainPage()) { Title = "Center", BarBackgroundColor = Color.FromHex("#2F6686"), BarTextColor = Color.FromHex("#FFFFFF") } };

                //MainPage = new NavigationPage(new LoginPage("", "", "Used"))
                //{ Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };
                // App.Current.MainPage = new MasterMainPage { Detail = new NavigationPage(new MainPage()) { Title = "Center", BarBackgroundColor = Color.FromHex("#2F6686"), BarTextColor = Color.FromHex("#FFFFFF") } };

            }

            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(new SelectLanguagePage(), true);
                });
                var timervalues = timerDB.GetTimerSetting();
                if (timervalues == null)
                    commonobj.addtimervalue();
            }
        }

    }
}

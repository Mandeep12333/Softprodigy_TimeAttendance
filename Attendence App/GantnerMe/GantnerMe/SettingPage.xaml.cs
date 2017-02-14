using GantnerMe.Interface;
using GantnerMe.Resx;
using GantnerMe.ViewModel;
using Plugin.SecureStorage;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GantnerMe
{
    public partial class SettingPage : ContentPage
    {
        internal readonly IMessageDialog messageDialog;
        public SettingPage()
        {
            InitializeComponent();
            //if (Device.OS == TargetPlatform.Android)
            //{
            //    DependencyService.Get<Isethasnavigationbar>().Show();
            //}
            NavigationPage.SetHasNavigationBar(this, false);
            Title = GlobalUserDetail.CompanyName.ToString();
            messageDialog = DependencyService.Get<IMessageDialog>();
            lblConfigurationLanguage.Text = Convert.ToString(GlobalLanguageCulture.SelectedLang);
            var ConfigLink = CrossSecureStorage.Current.GetValue("Url");
            configlink.Text = ConfigLink;
            ToolbarItems.Add(new ToolbarItem("Home", "home_ic.png", () =>
            {
                SetHomePage();

            }));

        }

        public async void HomeIconClick1(object sender, EventArgs e)
        {
            try
            {
                Homeicon.Opacity = .5;
                await Task.Delay(100);
                Homeicon.Opacity = 1;
                SetHomePage();
            }
            catch (Exception ex)
            {

            }
        }
        public async void Backiconclick(object sender, EventArgs e)
        {
            try
            {
                imgbackbutton.Opacity = .5;
                await Task.Delay(100);
                imgbackbutton.Opacity = 1;
                await Navigation.PopAsync(true);
            }
            catch (Exception ex)
            {

            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var ConfigLink = CrossSecureStorage.Current.GetValue("Url");
            configlink.Text = ConfigLink;

        }
        public void SetHomePage()
        {
            App.Current.MainPage = new MasterMainPage { Detail = new NavigationPage(new MainPage()) { Title = "Center", BarBackgroundColor = Color.FromHex("#2F6686"), BarTextColor = Color.FromHex("#FFFFFF") } };

        }
        public async void btnsubmitconfig(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            await App.Sleep(5000);
            messageDialog.SendToast(AppResources.recordupdated);
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        public async void OnTapGestureUrlClick(object sender, EventArgs args)
        {
            //imgediturl.Opacity = .5;
            await Task.Delay(100);
            // imgediturl.Opacity = 1;
            // imgediturl.IsEnabled = false;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushPopupAsync(new EditConfigurationPopupPage(), true);
            });
            // imgediturl.IsEnabled = true;

        }
        public async void OnTapGestureEditLanguageClick(object sender, EventArgs args)
        {
            // imgeditlanguage.Opacity = .5;
            await Task.Delay(100);
            // imgeditlanguage.Opacity = 1;
            // imgeditlanguage.IsEnabled = false;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushPopupAsync(new EditLanguagePage(), true);
            });
            //  imgeditlanguage.IsEnabled = true;

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }
    }
}

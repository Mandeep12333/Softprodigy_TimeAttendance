using GantnerMe.Class;
using GantnerMe.Helper;
using GantnerMe.Interface;
using GantnerMe.Resx;
using GantnerMe.ViewModel;
using ModernHttpClient;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.SecureStorage;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GantnerMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServerLinkPage : ContentPage
    {
        public OrganizationProfileDB db = new OrganizationProfileDB();
        public LoadingPopupPage loadingPage;
        internal readonly IMessageDialog messageDialog;
        public ServerLinkPage()
        {
            InitializeComponent();
            loadingPage = new LoadingPopupPage();
            messageDialog = DependencyService.Get<IMessageDialog>();
            SetLanguageCulture();
            if (Device.OS == TargetPlatform.iOS)
            {
                serverlinkboxview.IsVisible = false;
                // NavigationPage.SetHasNavigationBar(this, true);
            }
            if (Device.OS == TargetPlatform.Windows)
            {
                serverlinkboxview.IsVisible = false;
            }

            NavigationPage.SetHasNavigationBar(this, false);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void SetLanguageCulture()
        {
            string langCode = GlobalLanguageCulture.LanguageCode.ToString();
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo(langCode);
            L10n.SetLocale(ci);
            AppResources.Culture = ci;
            lblenterserverlink.Text = AppResources.EnterServerLink;
            // btnserversubmit.Text = AppResources.Next;
        }

        public async void btnServerSubmit(object sender, EventArgs e)
        {
            try
            {
                imgserversubmit.Opacity = .5;
                imgserversubmit.Opacity = 1;
                string ServerLink = txtserverlink.Text;
                if (string.IsNullOrWhiteSpace(ServerLink))
                {
                    txtserverlink.Text = string.Empty;
                    if (Device.OS == TargetPlatform.Windows)
                    {
                        await DisplayAlert("", AppResources.EnterServerLink, "OK");
                    }
                    else
                    {
                        messageDialog.SendToast(AppResources.EnterServerLink);
                    }

                }
                else if (!string.IsNullOrWhiteSpace(ServerLink))
                {
                    if (Uri.IsWellFormedUriString(ServerLink, UriKind.Absolute))
                    {
                        // Success//
                        bool Status = CrossConnectivity.Current.IsConnected;
                        if (Status == true)
                        {
                            GlobalUserDetail.ServerurlLink = ServerLink;
                            await Navigation.PushPopupAsync(loadingPage);
                            await App.Sleep(500);
                            using (var client = new HttpClient(new NativeMessageHandler()))
                            {
                                var RestUrl = string.Format(GlobalUserDetail.ServerurlLink + "OrganizationProfile");
                                client.BaseAddress = new Uri(RestUrl);
                                HttpResponseMessage response = await client.GetAsync(RestUrl);
                                if (response.IsSuccessStatusCode)
                                {
                                    CrossSecureStorage.Current.DeleteKey("Url");
                                    CrossSecureStorage.Current.SetValue("Url", ServerLink);
                                    var content = await response.Content.ReadAsStringAsync();
                                    var Getobjpost = JsonConvert.DeserializeObject<ClsOrganizationProfile>(content);
                                    if (Getobjpost != null)
                                    {

                                        string Logobase64string = Getobjpost.logo.src;
                                        string image = string.Empty;
                                        string Name = string.Empty;
                                        if (Logobase64string.Contains(","))
                                        {
                                            image = Logobase64string.Split(',')[1];
                                        }
                                        Name = Getobjpost.name;
                                        db.DeleteOrganizationProfile();
                                        OrganizationProfile objorg = new OrganizationProfile();
                                        objorg.OrganizationLogo = image;
                                        objorg.Companyname = Name;
                                        db.AddOrganizationProfile(objorg);
                                        GlobalUserDetail.CompanyName = Name;
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            await Navigation.RemovePopupPageAsync(loadingPage);
                                            // App.Current.MainPage = new LoginPage(image, Name);
                                            await Navigation.PushAsync(new LoginPage(image, Name, ""), true);
                                        });
                                    }

                                }
                                else
                                {
                                    await Navigation.RemovePopupPageAsync(loadingPage);
                                    await DisplayAlert(AppResources.Error, AppResources.failedtoconnecttoserver, "OK");
                                }
                            }

                        }
                        else
                        {
                            var Langcode = CrossSecureStorage.Current.GetValue("Url");
                            if (Langcode != null)
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await Navigation.RemovePopupPageAsync(loadingPage);
                                    // App.Current.MainPage = new LoginPage(image, Name);
                                    await Navigation.PushAsync(new LoginPage("", "", ""), true);
                                });
                            }
                            else
                            {
                                if (Device.OS == TargetPlatform.Windows)
                                {
                                    await DisplayAlert("", AppResources.checkyourinternetconnection, "OK");
                                }
                                else
                                {
                                    messageDialog.SendToast(AppResources.checkyourinternetconnection);
                                }
                            }
                        }

                    }
                    else
                    {
                        if (Device.OS == TargetPlatform.Windows)
                        {

                            await DisplayAlert("", AppResources.pleaseentervalidserverlink, "OK");
                        }
                        else
                        {
                            messageDialog.SendToast(AppResources.pleaseentervalidserverlink);
                        }
                        txtserverlink.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                await Navigation.RemovePopupPageAsync(loadingPage);
                await DisplayAlert(AppResources.Error, AppResources.failedtoconnecttoserver, "OK");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

        }

    }
}

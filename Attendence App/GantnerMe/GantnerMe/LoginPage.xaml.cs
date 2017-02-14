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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GantnerMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public UserDb UserDb = new UserDb();
        public OrganizationProfileDB db = new OrganizationProfileDB();
        public ReasonDB Reasondb = new ReasonDB();
        GetAllReasons objreasonslist;
        public tblReason _tblreason;
        internal readonly IMessageDialog messageDialog;
        internal readonly ICredentialsService storeService;
        public LoginPage(string CompanyLogo, string CompanyName, string cachedObject)
        {
            InitializeComponent();
            bool Status = CrossConnectivity.Current.IsConnected;
            if (Status == false)
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
                var getdetail = db.GetOrganizationProfile().ToList();
                if (getdetail.Count() > 0)
                {

                    Imagebitmap(getdetail[0].OrganizationLogo, getdetail[0].Companyname);
                    SetLanguageCulture();
                    ChnageControlsLanguage();
                    if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Windows)
                    {
                        boxviewusername.IsVisible = false;
                        boxviewPassword.IsVisible = false;
                    }
                    storeService = DependencyService.Get<ICredentialsService>();
                    messageDialog = DependencyService.Get<IMessageDialog>();
                    if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.Windows)
                    {
                        NavigationPage.SetHasNavigationBar(this, false);
                    }
                }
            }
            if (Status == true)
            {
                if (!string.IsNullOrEmpty(cachedObject))
                {
                    if (Status == false)
                    {
                        GetOrgProfileOffline();
                    }
                    else
                    {
                        // var loadingPage = new LoadingPopupPage();
                        //Navigation.PushPopupAsync(loadingPage);
                        GetOrgProfileOnline();
                        //Navigation.RemovePopupPageAsync(loadingPage);

                    }
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

                    GlobalLanguageCulture.LanguageCode = Langcode;

                    // GetAllReason();
                    SetLanguageCulture();
                    ChnageControlsLanguage();
                    if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Windows)
                    {
                        boxviewusername.IsVisible = false;
                        boxviewPassword.IsVisible = false;
                    }
                    storeService = DependencyService.Get<ICredentialsService>();
                    messageDialog = DependencyService.Get<IMessageDialog>();
                    NavigationPage.SetHasNavigationBar(this, false);
                }
                else
                {
                    // GetAllReason();
                    Imagebitmap(CompanyLogo, CompanyName);
                    SetLanguageCulture();
                    ChnageControlsLanguage();
                    if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Windows)
                    {
                        boxviewusername.IsVisible = false;
                        boxviewPassword.IsVisible = false;
                    }
                    storeService = DependencyService.Get<ICredentialsService>();
                    messageDialog = DependencyService.Get<IMessageDialog>();
                    NavigationPage.SetHasNavigationBar(this, false);

                }

            }
        }

        public async void GetOrgProfileOffline()
        {
            try
            {
                var getdetail = db.GetOrganizationProfile().ToList();
                if (getdetail.Count() > 0)
                {
                    GlobalUserDetail.CompanyName = getdetail[0].Companyname;
                    Byte[] ImageFotoBase64 = System.Convert.FromBase64String(getdetail[0].OrganizationLogo);
                    imgapilogo.Source = ImageSource.FromStream(() => new MemoryStream(ImageFotoBase64));
                    lblcompanyname.Text = getdetail[0].Companyname;
                }
            }

            catch (Exception ex)
            {
                await DisplayAlert("", ex.Message.ToString(), "OK");
            }
        }

        public async void GetOrgProfileOnline()
        {
            try
            {
                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    var RestUrl = string.Format(GlobalUserDetail.ServerurlLink + "OrganizationProfile");
                    client.BaseAddress = new Uri(RestUrl);
                    HttpResponseMessage response = await client.GetAsync(RestUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Getobjpost = JsonConvert.DeserializeObject<ClsOrganizationProfile>(content);
                        if (Getobjpost != null)
                        {
                            db.DeleteOrganizationProfile();
                            string Logobase64string = Getobjpost.logo.src;
                            string image = string.Empty;
                            string Name = string.Empty;
                            if (Logobase64string.Contains(","))
                            {
                                image = Logobase64string.Split(',')[1];
                            }
                            Name = Getobjpost.name;
                            OrganizationProfile objorg = new OrganizationProfile();
                            objorg.OrganizationLogo = image;
                            objorg.Companyname = Name;
                            db.AddOrganizationProfile(objorg);
                            GlobalUserDetail.CompanyName = Name;
                            Byte[] ImageFotoBase64 = System.Convert.FromBase64String(image);
                            imgapilogo.Source = ImageSource.FromStream(() => new MemoryStream(ImageFotoBase64));
                            lblcompanyname.Text = Name;
                        }

                    }
                    else
                    {
                        await DisplayAlert(AppResources.Error, AppResources.failedtoconnecttoserver, "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage = new NavigationPage(new ServerLinkPage())
                { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };

            }
        }

        public void Imagebitmap(string companylogo, string companyname)
        {
            string cFotoBase64 = companylogo;
            Byte[] ImageFotoBase64 = System.Convert.FromBase64String(cFotoBase64);
            imgapilogo.Source = ImageSource.FromStream(() => new MemoryStream(ImageFotoBase64));
            lblcompanyname.Text = companyname;
        }
        public void SetLanguageCulture()
        {
            string langCode = GlobalLanguageCulture.LanguageCode.ToString();
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo(langCode);
            L10n.SetLocale(ci);
            AppResources.Culture = ci;
        }
        public void ChnageControlsLanguage()
        {
            lblcredentialsName.Text = AppResources.enterlogincredentials;
            txtUserName.Placeholder = AppResources.username;
            txtpassword.Placeholder = AppResources.password;
            // btnlogin.Text = AppResources.login;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        // Get data from Api//

        // Call Api to Get Token//
        public async void btnloginclick(object sender, EventArgs e)
        {
            btnlogin.Opacity = .5;
            btnlogin.Opacity = 1;
            var loadingPage = new LoadingPopupPage();
            try
            {
                string UserName = txtUserName.Text;
                string Password = txtpassword.Text;
                if (string.IsNullOrWhiteSpace(UserName))
                {
                    if (Device.OS == TargetPlatform.Windows)
                    {
                        await DisplayAlert("", AppResources.enterusername, "OK");
                    }
                    else
                    {
                        messageDialog.SendToast(AppResources.enterusername);
                    }
                }
                else if (string.IsNullOrWhiteSpace(Password))
                {
                    if (Device.OS == TargetPlatform.Windows)
                    {
                        await DisplayAlert("", AppResources.enterpassword, "OK");
                    }
                    else
                    {
                        messageDialog.SendToast(AppResources.enterpassword);
                    }
                }
                else
                {
                    bool Status = CrossConnectivity.Current.IsConnected;
                    if (Status == true)
                    {
                        await Navigation.PushPopupAsync(loadingPage);
                        await App.Sleep(2000);
                        LoginModelGanter objlogin = new LoginModelGanter();
                        objlogin.userName = UserName.Trim();
                        objlogin.password = Password.Trim();
                        using (var client = new HttpClient(new NativeMessageHandler()))
                        {
                            Uri baseAddress = new Uri(GlobalUserDetail.ServerurlLink + "Login");
                            client.BaseAddress = baseAddress;
                            var json = JsonConvert.SerializeObject(objlogin);
                            var content = new StringContent(json, Encoding.UTF8, "text/json");
                            HttpResponseMessage response = null;
                            response = await client.PostAsync(baseAddress, content);
                            if (response.IsSuccessStatusCode)
                            {
                                var Res = await response.Content.ReadAsStringAsync();
                                string Res1 = Res.Replace("\"", "");
                                CrossSecureStorage.Current.DeleteKey("UserName");
                                CrossSecureStorage.Current.SetValue("UserName", UserName);
                                CrossSecureStorage.Current.DeleteKey("Password");
                                CrossSecureStorage.Current.SetValue("Password", Password);
                                GlobalUserDetail.GlobalGUID = new Guid(Res1);
                                UserDb.DeleteUser();
                                tblUser objuser = new tblUser();
                                objuser.Password = Password.Trim();
                                objuser.Username = UserName.Trim();
                                objuser.UserToken = GlobalUserDetail.GlobalGUID;
                                UserDb.AddUsers(objuser);
                                if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.iOS)
                                {

                                    // CrossSecureStorage.Current.SetValue("Password", Password.Trim());
                                    // CrossSecureStorage.Current.SetValue("UserID", "1");
                                    //DependencyService.Get<ICredentialsService>().DeleteCredentials();
                                    //bool doCredentialsExist = storeService.DoCredentialsExist();
                                    //if (!doCredentialsExist)
                                    //{
                                    //    storeService.SaveCredentials(UserName.Trim(), Password.Trim(), "1");
                                    //}
                                }
                                else
                                {
                                    CrossSecureStorage.Current.SetValue("UserName", UserName.Trim());
                                    CrossSecureStorage.Current.SetValue("Password", Password.Trim());
                                    CrossSecureStorage.Current.SetValue("UserID", "1");
                                }

                                await Navigation.RemovePopupPageAsync(loadingPage);
                                App.Current.MainPage = new MasterMainPage
                                {
                                    Detail = new NavigationPage(new MainPage())
                                    {
                                        Title = "Center",
                                        BarBackgroundColor = Color.FromHex("#498BA5"),
                                        BarTextColor = Color.FromHex("#FFFFFF")
                                    }
                                };

                                //  Application.Current.MainPage = new MasterMainPage();
                                // await Navigation.PushModalAsync(new MasterMainPage(), true);
                                //  App.Current.MainPage =new MasterMainPage();
                                // Application.Current.MainPage = new MasterMainPage();
                                // Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new NavigationPage(new MasterMainPage()));
                                // Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new NavigationPage(new MasterMainPage()) { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") });
                                txtUserName.Text = string.Empty;
                                txtpassword.Text = string.Empty;
                                btnlogin.IsEnabled = true;
                            }
                            else
                            {
                                await Navigation.RemovePopupPageAsync(loadingPage);
                                await DisplayAlert("", AppResources.Loginfailed, "OK");

                            }
                        }


                    }
                    else
                    {
                        var Langcode = CrossSecureStorage.Current.GetValue("Url");
                        if (Langcode != null)
                        {
                            await Navigation.PushPopupAsync(loadingPage);
                            var checkuserdetail = UserDb.CheckUserName(UserName, Password);
                            if (checkuserdetail != null)
                            {
                                GlobalUserDetail.GlobalGUID = checkuserdetail.UserToken;
                                App.Current.MainPage = new MasterMainPage { Detail = new NavigationPage(new MainPage()) { Title = "Center", BarBackgroundColor = Color.FromHex("#2F6686"), BarTextColor = Color.FromHex("#FFFFFF") } };
                                await Navigation.RemovePopupPageAsync(loadingPage);
                            }
                            else
                            {
                                await DisplayAlert("", AppResources.Loginfailed, "OK");
                                await Navigation.RemovePopupPageAsync(loadingPage);
                            }
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

            }
            catch (Exception ex)
            {
                await Navigation.RemovePopupPageAsync(loadingPage);
                App.Current.MainPage = new NavigationPage(new ServerLinkPage())
                { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };

            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

    }
}

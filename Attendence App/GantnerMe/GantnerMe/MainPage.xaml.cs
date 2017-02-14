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
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GantnerMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MasterDetailPage objmaster;
        public UserDb UserDb = new UserDb();
        public ReasonDB Reasondb = new ReasonDB();
        public OrganizationProfileDB db = new OrganizationProfileDB();
        public AsignedLocationDB AsignedDB = new AsignedLocationDB();
        public Common Commonobj = new Common();
        GetAllReasons objreasonslist;
        public tblReason _tblreason;
        internal readonly IMessageDialog messageDialog;
        internal readonly ICredentialsService storeService;
        public MainPage()
        {
            InitializeComponent();
            objmaster = new MasterDetailPage();

            bool Status = CrossConnectivity.Current.IsConnected;
            if (Status == false)
            {
                var Locations = AsignedDB.GetAsignedLocation().ToList();
                if (Locations.Count() > 0)
                {
                    string loca = Locations[0].mapLocation;
                    var AsignedLocation = JsonConvert.DeserializeObject<Location>(loca);
                    App.LocationList = null;
                    if (AsignedLocation != null)
                    {
                        App.LocationList = AsignedLocation;
                    }
                }
            }
            else
            {

                //  GetAsignedLocation();
                // GetAllReason();

            }
            var Gettitle = db.GetOrganizationProfile().ToList();
            // Title = Gettitle[0].Companyname;
            GlobalUserDetail.CompanyName = Gettitle[0].Companyname;
            SetLanguageCulture();
            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.Android)
            {
                NavigationPage.SetHasNavigationBar(this, false);

            }
            if (Device.OS == TargetPlatform.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
            messageDialog = DependencyService.Get<IMessageDialog>();
            ToolbarItems.Add(new ToolbarItem("Home", "home_ic.png", () =>
            {
                SetHomePage();

            }));
            // OpenMasterPage();
        }
        public void SetHomePage()
        {
            App.Current.MainPage = new MasterMainPage { Detail = new NavigationPage(new MainPage()) { Title = "Center", BarBackgroundColor = Color.FromHex("#2F6686"), BarTextColor = Color.FromHex("#FFFFFF") } };

        }
        public async void OpenMasterPage()
        {
            //await Navigation.PushAsync(new MasterMainPage(), true);
            // await Navigation.PushAsync(new MasterPage(), true);
            await Navigation.PushModalAsync(new MasterMainPage(), true);
        }


        public async void GlobalMessage()
        {
            if (!string.IsNullOrWhiteSpace(Convert.ToString(GlobalUserDetail.CheckInOut)))
            {
                if (Device.OS == TargetPlatform.Windows)
                {
                    await DisplayAlert("", AppResources.ReuqestSaved, "OK");
                }
                else
                {
                    messageDialog.SendToast(AppResources.ReuqestSaved);
                }
            }
            if (!string.IsNullOrWhiteSpace(Convert.ToString(GlobalUserDetail.EditLanguage)))
            {
                if (Device.OS == TargetPlatform.Windows)
                {
                    await DisplayAlert("", AppResources.ReuqestSaved, "OK");
                }
                else
                {
                    messageDialog.SendToast(AppResources.ReuqestSaved);
                }
            }
            GlobalUserDetail.Settings = string.Empty;
            GlobalUserDetail.UserProfile = string.Empty;
            GlobalUserDetail.Info = string.Empty;
            GlobalUserDetail.CheckInOut = string.Empty;
            GlobalUserDetail.EditLanguage = string.Empty;

        }


        public async void GetAllReason()
        {
            var loadingPage = new LoadingPopupPage();
            try
            {
                bool Status = CrossConnectivity.Current.IsConnected;
                if (Status == true)
                {

                    await Navigation.PushPopupAsync(loadingPage);
                    using (var client = new HttpClient(new NativeMessageHandler()))
                    {
                        //var RestUrl = string.Format(GlobalUserDetail.ServerurlLink +"/gatac_api/mobile/Reasons");
                        var RestUrl = string.Format(GlobalUserDetail.ServerurlLink + "Reasons");
                        // var RestUrl = string.Format("https://api.stubhub.com/search/inventory/v2/?eventId=9640832&sectionStats=true&zoneStats=true&start=0&allSectionZoneStats=false&quantity=1&rows=20&priceType=currentPrice&priceMin=100&Bearer=kqN9BPBB7yZRKHlEmq3Au1FkA2Ua");
                        client.BaseAddress = new Uri(RestUrl);
                        HttpResponseMessage response = await client.GetAsync(RestUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var GetReasons = JsonConvert.DeserializeObject<GetAllReasons[]>(content);
                            if (GetReasons.Count() > 0)
                            {
                                Reasondb.DeleteReasons();
                                App.GetReasonlist.Clear();
                                foreach (var item in GetReasons)
                                {

                                    string Lang = GlobalLanguageCulture.LanguageCode;
                                    string Name = item.name.Replace(@"\", " ");
                                    var Reasonnames = JsonConvert.DeserializeObject<ReasonLangCode>(Name);
                                    string namevalue = string.Empty;
                                    if (Reasonnames != null)
                                    {
                                        if (Lang == "ar")
                                        {
                                            namevalue = Reasonnames.ar;
                                        }
                                        else
                                        {
                                            namevalue = Reasonnames.en;
                                        }
                                    }
                                    _tblreason = new tblReason();
                                    _tblreason.codeIn = item.codeIn;
                                    _tblreason.codeOut = item.codeOut;
                                    _tblreason.Reasonid = item.id;
                                    _tblreason.name = namevalue;
                                    objreasonslist = new GetAllReasons();
                                    objreasonslist.codeIn = item.codeIn;
                                    objreasonslist.codeOut = item.codeOut;
                                    objreasonslist.id = item.id;
                                    objreasonslist.name = namevalue;
                                    App.GetReasonlist.Add(objreasonslist);
                                    Reasondb.AddReasons(_tblreason);
                                }
                            }
                            await Navigation.RemovePopupPageAsync(loadingPage);

                        }
                        else
                        {
                            await Navigation.RemovePopupPageAsync(loadingPage);
                            App.Current.MainPage = new NavigationPage(new ServerLinkPage())
                            { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };

                        }
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
            catch (Exception ex)
            {
                await Navigation.RemovePopupPageAsync(loadingPage);
                App.Current.MainPage = new NavigationPage(new ServerLinkPage())
                { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };

            }
        }


        public async void humbergimageclick(object sender1, EventArgs e)
        {
            try
            {
                
                imghumburg.Opacity = .5;
                // MessagingCenter.Send<MainPage>(this, "Hi");
                await Task.Delay(100);
                imghumburg.Opacity = .5;
                (App.Current.MainPage as MasterDetailPage).IsPresented = true;
            }
            catch (Exception ex)
            {

            }
        }

        public void HomeIconClick(object sender, EventArgs e)
        {
            try
            {
                SetHomePage();
            }
            catch (Exception ex)
            {

            }
        }

        public void SetLanguageCulture()
        {
            string langCode = GlobalLanguageCulture.LanguageCode.ToString();
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo(langCode);
            L10n.SetLocale(ci);
            AppResources.Culture = ci;
            lblcheckincheckout.Text = AppResources.CheckInCheckout;
            lblmyattendence.Text = AppResources.myattendence;
            lblselectoption.Text = AppResources.selectoption;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.OS == TargetPlatform.Android)
            {
                DependencyService.Get<Isethasnavigationbar>().Hide();
            }
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                SetLanguageCulture();
                bool Status = CrossConnectivity.Current.IsConnected;
                if (Status == true)
                {
                    GetAllReason();
                    UpdateUserToken();
                    var Locationsvalue = AsignedDB.GetAsignedLocation().ToList();
                    var reasonsvalue = Reasondb.GetReasons().ToList();
                    var GetOrgProfile = db.GetOrganizationProfile().ToList();
                    if (Commonobj.checktimercall("reasons") || reasonsvalue.Count() == 0)
                    {
                        GetAllReason();
                        Commonobj.updatetimer("reasons");
                    }

                    //if (Commonobj.checktimercall("assignedlocations") || Locationsvalue.Count() == 0)
                    //{
                    //    GetAsignedLocation();
                    //    Commonobj.updatetimer("assignedlocations");
                    //}
                    if (Commonobj.checktimercall("organizationprofile") || GetOrgProfile.Count() == 0)
                    {
                        GetOrgProfileOnline();
                        Commonobj.updatetimer("organizationprofile");
                    }
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage = new NavigationPage(new ServerLinkPage())
                { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };
            }
        }

        public async void UpdateUserToken()
        {
            var loadingPage = new LoadingPopupPage();

            try
            {
                await Navigation.PushPopupAsync(loadingPage);
                string Username = CrossSecureStorage.Current.GetValue("UserName");
                string Password = CrossSecureStorage.Current.GetValue("Password");
                LoginModelGanter objlogin = new LoginModelGanter();
                objlogin.userName = Username;
                objlogin.password = Password;
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
                        GlobalUserDetail.GlobalGUID = new Guid(Res1);
                        UserDb.DeleteUser();
                        tblUser objuser = new tblUser();
                        objuser.Password = Password;
                        objuser.Username = Username;
                        objuser.UserToken = GlobalUserDetail.GlobalGUID;
                        UserDb.AddUsers(objuser);
                        await Navigation.RemovePopupPageAsync(loadingPage);

                    }
                    else
                    {
                        await Navigation.RemovePopupPageAsync(loadingPage);
                        App.Current.MainPage = new NavigationPage(new ServerLinkPage())
                        { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };
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

        public async void GetOrgProfileOnline()
        {
            try
            {
                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    //  var RestUrl = string.Format(GlobalUserDetail.ServerurlLink + "/gatac_api/mobile/OrganizationProfile");
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


        public void BtncheckinCheckout(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushPopupAsync(loadingPage);
                await Navigation.PushAsync(new CheckInOutPage(), true);
                await Navigation.RemovePopupPageAsync(loadingPage);
            });

        }

        private void btnmyattendende(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushPopupAsync(loadingPage);
                await Navigation.PushAsync(new MyAttendencePage(), true);
                await Navigation.RemovePopupPageAsync(loadingPage);
            });
        }

        public async void Logout()
        {
            bool _result;
            _result = await DisplayAlert("Logout", "Are you Sure want to logout?", "Confirm", "Cancel");
            if (_result)
            {
                try
                {
                    if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.iOS)
                    {
                        GlobalUserDetail.RatePresentation = null;
                        GlobalUserDetail.LoginSuccessMessage = null;
                        GlobalUserDetail.RegisterSuccess = null;
                        GlobalUserDetail.EventFeedbackMessage = null;
                        GlobalUserDetail.UserID = null;
                        GlobalUserDetail.Email = null;
                        DependencyService.Get<ICredentialsService>().DeleteCredentials();
                    }
                    else
                    {
                        GlobalUserDetail.RatePresentation = null;
                        GlobalUserDetail.LoginSuccessMessage = null;
                        GlobalUserDetail.RegisterSuccess = null;
                        GlobalUserDetail.EventFeedbackMessage = null;
                        GlobalUserDetail.UserID = null;
                        GlobalUserDetail.Email = null;
                        CrossSecureStorage.Current.DeleteKey("UserName");
                        CrossSecureStorage.Current.DeleteKey("Password");
                        CrossSecureStorage.Current.DeleteKey("UserID");

                    }
                    var loadingPage = new LoadingPopupPage();
                    await Navigation.PushPopupAsync(loadingPage);
                    await App.Sleep(2000);
                    await Navigation.RemovePopupPageAsync(loadingPage);
                    Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new NavigationPage(new StartPage()) { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#316888") });
                }
                catch (Exception ex)
                {
                    var msg = ex.Message.ToString();
                    await DisplayAlert("Error", ex.Message.ToString(), "OK");
                }

            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }
    }
}

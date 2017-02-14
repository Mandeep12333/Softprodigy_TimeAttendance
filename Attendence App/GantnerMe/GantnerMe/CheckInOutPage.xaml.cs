using GantnerMe.Class;
using GantnerMe.Interface;
using GantnerMe.Resx;
using GantnerMe.ViewModel;
using ModernHttpClient;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
    public partial class CheckInOutPage : ContentPage
    {
        internal readonly IMessageDialog messageDialog;
        public AsignedLocationDB AsignedDB = new AsignedLocationDB();
        public Common Commonobj = new Common();
        public CheckInOutPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //if (Device.OS == TargetPlatform.Android)
            //{
            //    DependencyService.Get<Isethasnavigationbar>().Show();
            //}
            SetLanguageCulture();
            //var tapGestureRecognizer = new TapGestureRecognizer();
            //tapGestureRecognizer.Tapped += async(s, e) => {
            //    imageHomeicon.Opacity = .5;
            //    await Task.Delay(200);
            //    imageHomeicon.Opacity = 1;
            //    SetHomePage();
            //};
            //imageHomeicon.GestureRecognizers.Add(tapGestureRecognizer);
            Title = GlobalUserDetail.CompanyName.ToString();
            messageDialog = DependencyService.Get<IMessageDialog>();
            ToolbarItems.Add(new ToolbarItem("Home", "home_ic.png", () =>
            {
                SetHomePage();

            }));
        }

        public void SetHomePage()
        {
            App.Current.MainPage = new MasterMainPage { Detail = new NavigationPage(new MainPage()) { Title = "Center", BarBackgroundColor = Color.FromHex("#2F6686"), BarTextColor = Color.FromHex("#FFFFFF") } };

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
            GlobalMessage();
            bool Status = CrossConnectivity.Current.IsConnected;
            if (Status == true)
            {
                var Locationsvalue = AsignedDB.GetAsignedLocation().ToList();
                if (Commonobj.checktimercall("assignedlocations") || Locationsvalue.Count() == 0)
                {
                    GetAsignedLocation();
                    Commonobj.updatetimer("assignedlocations");
                }
            }

        }
        public async void GetAsignedLocation()
        {
            var loadingPage = new LoadingPopupPage();
            try
            {
                await Navigation.PushPopupAsync(loadingPage);
                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    //GlobalUserDetail.ServerurlLink + "/gatac_api/mobile
                    //var RestUrl = string.Format(GlobalUserDetail.ServerurlLink+"/gatac_api/mobile" +"/"+GlobalUserDetail.GlobalGUID + "/AssignedLocations");
                    var RestUrl = string.Format(GlobalUserDetail.ServerurlLink + GlobalUserDetail.GlobalGUID + "/AssignedLocations");
                    client.BaseAddress = new Uri(RestUrl);
                    HttpResponseMessage response = await client.GetAsync(RestUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Getobjpost = JsonConvert.DeserializeObject<AsignedLocation[]>(content);
                        if (Getobjpost.Count() > 0)
                        {
                            foreach (var item in Getobjpost)
                            {
                                string Desc = item.description;
                                var Loc = item.mapLocation;
                                var AsignedLocation = JsonConvert.DeserializeObject<Location>(Loc);
                                App.LocationList = null;
                                if (AsignedLocation != null)
                                {
                                    App.LocationList = AsignedLocation;
                                }
                                string Name = item.name;
                                AsignedDB.DeleteAsignedLocation();
                                tblAsignedLocation objasigned = new tblAsignedLocation();
                                objasigned.description = item.description;
                                objasigned.mapLocation = item.mapLocation;
                                objasigned.name = item.name;
                                AsignedDB.AddAsignedLocation(objasigned);

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
            catch (Exception ex)
            {
                await Navigation.RemovePopupPageAsync(loadingPage);
                App.Current.MainPage = new NavigationPage(new ServerLinkPage())
                { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };
            }
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
            GlobalUserDetail.Settings = string.Empty;
            GlobalUserDetail.UserProfile = string.Empty;
            GlobalUserDetail.Info = string.Empty;
            GlobalUserDetail.CheckInOut = string.Empty;
            GlobalUserDetail.EditLanguage = string.Empty;

        }
        public void SetLanguageCulture()
        {
            string langCode = GlobalLanguageCulture.LanguageCode.ToString();
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo(langCode);
            L10n.SetLocale(ci);
            AppResources.Culture = ci;
            lblpleasechoosefunction.Text = AppResources.pleasechoseafunction;
            lblComingCheckIn.Text = AppResources.comingcheckin;
            lblReason.Text = AppResources.reasons;
            lblcheckoutpopup.Text = AppResources.leavingcheckout;
        }

        public async void OnTapGestureShowMapCheckIn1(object sender, EventArgs args)
        {
            lblComingCheckIn.Opacity = .5;
            await Task.Delay(100);
            lblComingCheckIn.Opacity = 1;
            lblComingCheckIn.IsEnabled = false;
            GetPermision();
            lblComingCheckIn.IsEnabled = true;
        }

        public async void LocationisOn()
        {

            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            //await Navigation.PushAsync(new CheckInCheckOutMapPage("CheckIn"), true);
            NavigationPage NP = ((NavigationPage)((MasterDetailPage)App.Current.MainPage).Detail);
            await NP.PushAsync(new CheckInCheckOutMapPage("CheckIn"), true);
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        public async void GetPermision()
        {
            try
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    if (CrossGeolocator.Current.IsGeolocationEnabled == false)
                    {
                        if (Device.OS == TargetPlatform.Windows)
                        {
                            await DisplayAlert("Location Need", "Need your location.Please turn on your GPS", "OK");
                        }
                        else
                        {
                            messageDialog.SendToast("Need your location.Please turn on your GPS.");
                        }
                    }
                    else
                    {
                        var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                        if (status != PermissionStatus.Granted)
                        {
                            if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                            {
                                await DisplayAlert("Location Need", "Need your location.Please turn on your GPS.", "OK");
                            }

                            var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                            status = results[Permission.Location];
                        }

                        if (status == PermissionStatus.Granted)
                        {
                            LocationisOn();
                        }
                        else if (status != PermissionStatus.Unknown)
                        {
                            await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                        }
                    }
                }
                if (Device.OS == TargetPlatform.iOS)
                {
                    if (CrossGeolocator.Current.IsGeolocationEnabled == false)
                    {
                        messageDialog.SendToast("Need your location.Please turn on your GPS.");
                    }
                    else
                    {
                        LocationisOn();
                    }
                }

                if (Device.OS == TargetPlatform.Windows)
                {

                }

            }
            catch (Exception ex)
            {

                await DisplayAlert("Location Denied", ex.Message.ToString(), "OK");
            }
        }

        public async void OnTapGestureShowReasonPopup(object sender, EventArgs e)
        {
            lblReason.Opacity = .5;
            await Task.Delay(100);
            lblReason.Opacity = 1;
            lblReason.IsEnabled = false;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushPopupAsync(new ReasonsPage(), true);
            });
            lblReason.IsEnabled = true;
        }

        public async void OnTapGestureCheckOutLeavingPopup(object sender, EventArgs e)
        {
            lblcheckoutpopup.Opacity = .5;
            await Task.Delay(100);
            lblcheckoutpopup.Opacity = 1;
            lblcheckoutpopup.IsEnabled = false;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushPopupAsync(new ReasonsEmojiPopup(), true);
            });
            lblcheckoutpopup.IsEnabled = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }
    }
}

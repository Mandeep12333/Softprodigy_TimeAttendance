using GantnerMe.Class;
using GantnerMe.Helper;
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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace GantnerMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckInCheckOutMapPage : ContentPage
    {
        public static Geolocation objgeolocation = new Geolocation();
        public CheckInCheckOutDB _CheckInCheckOutDB = new CheckInCheckOutDB();
        public tblCheckInCheckOut _tblCheckInOut;
        public UploadBookingDB _UploadBookingDB = new UploadBookingDB();
        public string LocationJson = string.Empty;
        internal readonly IMessageDialog messageDialog;
        public OrganizationProfileDB _OrganizationProfileDB = new OrganizationProfileDB();
        Geocoder geoCoder;
        public class ClsAddress
        {
            public string Address { get; set; }
        }
        public class Geolocation
        {
            public string lat { get; set; }
            public string lng { get; set; }
        }



        public CheckInCheckOutMapPage(string CheckInCheckOutType)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //if (Device.OS == TargetPlatform.Android)
            //{
            //    DependencyService.Get<Isethasnavigationbar>().Show();
            //}
            bool Status = CrossConnectivity.Current.IsConnected;
            if (Status == false)
            {
                var getCurrentLocation = _CheckInCheckOutDB.GetCheckInOutLocation().ToList();
                if (getCurrentLocation.Count() > 0)
                {
                    //objgeolocation.lat = getCurrentLocation[0].lat;
                    // objgeolocation.lng = getCurrentLocation[0].lng;
                    var MapPosition = new Position(Convert.ToDouble(getCurrentLocation[0].lat), Convert.ToDouble(getCurrentLocation[0].lng));
                    var pin = new Pin();
                    pin.Type = PinType.Place;
                    pin.Position = new Position(Convert.ToDouble(getCurrentLocation[0].lat), Convert.ToDouble(getCurrentLocation[0].lng));
                    pin.Label = getCurrentLocation[0].Address;
                    pin.Address = getCurrentLocation[0].Address;
                    MyMap.IsVisible = true;
                    submit.IsVisible = true;
                    cancel.IsVisible = true;
                    MyMap.Pins.Add(pin);
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(MapPosition, Distance.FromMiles(1)));
                }
                else
                {
                    MyMap.IsVisible = false;
                    submit.IsVisible = false;
                    cancel.IsVisible = false;
                    DisplayAlert("", AppResources.checkyourinternetconnection, "OK");
                }
            }
            else
            {
                MapBind();
            }
            var Gettitle = _OrganizationProfileDB.GetOrganizationProfile().ToList();
            Title = Gettitle[0].Companyname;
            SetLanguageCulture();
            GlobalUserDetail.CheckInCheckOutType = Convert.ToString(CheckInCheckOutType);
            messageDialog = DependencyService.Get<IMessageDialog>();
            geoCoder = new Geocoder();
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

        public void SetHomePage()
        {
            App.Current.MainPage = new MasterMainPage { Detail = new NavigationPage(new MainPage()) { Title = "Center", BarBackgroundColor = Color.FromHex("#2F6686"), BarTextColor = Color.FromHex("#FFFFFF") } };

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                bool Status = CrossConnectivity.Current.IsConnected;
                if (Status == true)
                {
                    CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
                    CrossGeolocator.Current.PositionError += Current_PositionError;
                    if (CrossGeolocator.Current.IsListening)
                    {
                        await CrossGeolocator.Current.StopListeningAsync();
                    }
                    else
                    {
                        await CrossGeolocator.Current.StartListeningAsync(30000, 0);
                    }
                }
            }
            catch
            {

            }
        }

        // Change Map Location with pin from Current Position Error//

        private void Current_PositionError(object sender, Plugin.Geolocator.Abstractions.PositionErrorEventArgs e)
        {
            var msg = e.Error.ToString();
        }

        // Change Map Location with pin from Current Position//
        private async void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            try
            {
                bool Status = CrossConnectivity.Current.IsConnected;
                if (Status == true)
                {
                    string CurrentAddress = string.Empty;
                    var position = e.Position;
                    double lat = position.Latitude;
                    double Long = position.Longitude;
                    var MapPosition = new Position(lat, Long);
                    var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(MapPosition);
                    if (possibleAddresses.Any())
                    {
                        string Address = possibleAddresses.First();
                        CurrentAddress = Regex.Replace(Address, @"\t|\n|\r", " ");
                    }

                    // save data into temp table//
                    _CheckInCheckOutDB.DeleteCheckInOutLocation();
                    _tblCheckInOut = new tblCheckInCheckOut();
                    _tblCheckInOut.lat = position.Latitude.ToString();
                    _tblCheckInOut.lng = position.Longitude.ToString();
                    _tblCheckInOut.Address = CurrentAddress;
                    _CheckInCheckOutDB.AddCheckInOutLocation(_tblCheckInOut);
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(Convert.ToDouble(position.Latitude), Convert.ToDouble(position.Longitude)),
                        Label = CurrentAddress,
                        Address = CurrentAddress
                    };
                    MyMap.IsVisible = true;
                    submit.IsVisible = true;
                    cancel.IsVisible = true;
                    MyMap.Pins.Clear();
                    MyMap.Pins.Add(pin);
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(MapPosition, Distance.FromMiles(1)));

                }
            }
            catch (Exception ex)
            {

            }
        }

        // Set language culture//
        public void SetLanguageCulture()
        {
            string langCode = GlobalLanguageCulture.LanguageCode.ToString();
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo(langCode);
            L10n.SetLocale(ci);
            AppResources.Culture = ci;
            // submit.Text = AppResources.submit;
            // cancel.Text = AppResources.cancel;
        }

        // Bind Map with Current latt and Long//
        public async void MapBind()
        {
            var loadingPage = new LoadingPopupPage();
            try
            {

                string CurrentAddress = string.Empty;
                if (CrossGeolocator.Current.IsGeolocationEnabled == false)
                {
                    if (Device.OS == TargetPlatform.Windows)
                    {
                        await DisplayAlert("Need location", "Gunna need that location.Please on your GPS.", "OK");
                    }
                    else
                    {
                        messageDialog.SendToast("Gunna need that location.Please on your GPS.");
                    }
                    MyMap.IsVisible = false;
                    submit.IsVisible = false;
                    cancel.IsVisible = false;
                }
                else
                {

                    await Navigation.PushPopupAsync(loadingPage);
                    MyMap.IsVisible = false;
                    submit.IsVisible = false;
                    cancel.IsVisible = false;
                    var locator = CrossGeolocator.Current;
                    if (Device.OS == TargetPlatform.iOS)
                    {
                        locator.AllowsBackgroundUpdates = true;
                    }
                    locator.DesiredAccuracy = 100; //50
                    List<ClsAddress> List = new List<ClsAddress>();
                    ClsAddress objadress;
                    var position = await locator.GetPositionAsync(timeoutMilliseconds: 20000); //10000
                    if (position != null)
                    {
                        _CheckInCheckOutDB.DeleteCheckInOutLocation();
                        _tblCheckInOut = new tblCheckInCheckOut();
                        objgeolocation.lat = position.Latitude.ToString();
                        objgeolocation.lng = position.Longitude.ToString();
                        var MapPosition = new Position(Convert.ToDouble(position.Latitude), Convert.ToDouble(position.Longitude));
                        //CancellationTokenSource ctx = new CancellationTokenSource();
                        //ctx.CancelAfter(30000);
                        var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(MapPosition);
                        if (possibleAddresses.Any())
                        {
                            string Address = possibleAddresses.First();
                            CurrentAddress = Regex.Replace(Address, @"\t|\n|\r", " ");
                        }
                        _tblCheckInOut.lat = position.Latitude.ToString();
                        _tblCheckInOut.lng = position.Longitude.ToString();
                        _tblCheckInOut.Address = CurrentAddress;
                        _CheckInCheckOutDB.AddCheckInOutLocation(_tblCheckInOut);
                        if (possibleAddresses != null)
                        {
                            foreach (var address in possibleAddresses)
                            {
                                objadress = new ClsAddress();
                                objadress.Address = address;
                                List.Add(objadress);
                            }
                        }
                        if (List.Count > 0)
                        {
                            string AddressList0 = string.Empty;
                            string Location = string.Empty;
                            string AddressList1 = string.Empty;
                            string FullAddress = string.Empty;
                            if (List[0] != null)
                            {
                                AddressList0 = List[0].Address;
                                Location = Regex.Replace(AddressList0, @"\t|\n|\r", " ");

                            }

                            var GetAsignedlocation = App.LocationList;

                            var pin = new Pin();
                            pin.Type = PinType.Place;
                            pin.Position = new Position(Convert.ToDouble(position.Latitude), Convert.ToDouble(position.Longitude));
                            pin.Label = CurrentAddress;
                            pin.Address = CurrentAddress;
                            MyMap.IsVisible = true;
                            submit.IsVisible = true;
                            cancel.IsVisible = true;
                            MyMap.Pins.Add(pin);
                            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(MapPosition, Distance.FromMiles(1)));
                            await Navigation.RemovePopupPageAsync(loadingPage);
                        }
                        else
                        {
                            var pin = new Pin
                            {
                                Type = PinType.Place,
                                Position = new Position(Convert.ToDouble(position.Latitude), Convert.ToDouble(position.Longitude)),
                                Label = CurrentAddress,
                                Address = CurrentAddress
                            };
                            MyMap.Circle = new Model.CustomCircle
                            {
                                Position = new Position(Convert.ToDouble(position.Latitude), Convert.ToDouble(position.Longitude)),
                                Radius = 1000
                            };
                            MyMap.IsVisible = true;
                            submit.IsVisible = true;
                            cancel.IsVisible = true;
                            MyMap.Pins.Add(pin);
                            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(MapPosition, Distance.FromMiles(1)));
                            await Navigation.RemovePopupPageAsync(loadingPage);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // await Navigation.RemovePopupPageAsync(loadingPage);
                string Location = string.Empty;
                var msg = ex.Message.ToString();
                double latlong = 30.713942;
                double longtitude = 76.696130;
                objgeolocation.lat = latlong.ToString();
                objgeolocation.lng = longtitude.ToString();
                _CheckInCheckOutDB.DeleteCheckInOutLocation();
                _tblCheckInOut = new tblCheckInCheckOut();
                _tblCheckInOut.lat = latlong.ToString();
                _tblCheckInOut.lng = longtitude.ToString();

                var MapPosition1 = new Position(latlong, longtitude);
                //var possibleAddresses1 = await geoCoder.GetAddressesForPositionAsync(MapPosition1);
                //if (possibleAddresses1.Any())
                //{
                //    Location = possibleAddresses1.First();
                //}
                Location = "E-206, Phase 8 B, Industrial Area, Sahibzada Ajit Singh Nagar, Punjab ";
                _tblCheckInOut.Address = Location;
                _CheckInCheckOutDB.AddCheckInOutLocation(_tblCheckInOut);
                var pin1 = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(30.713942, 76.696130),
                    Label = Location,
                    Address = Location
                };
                MyMap.IsVisible = true;
                submit.IsVisible = true;
                cancel.IsVisible = true;
                MyMap.Pins.Add(pin1);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(MapPosition1, Distance.FromMiles(1)));
                await Navigation.RemovePopupPageAsync(loadingPage);
                // await DisplayAlert("", ex.Message.ToString(), "OK");
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var position = e.Position;
            double lat = position.Latitude;
            double Long = position.Longitude;
            // throw new NotImplementedException();
        }

        // save Upload Booking Request  to server//
        private async void BtnSubmit(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            try
            {
                bool Status = CrossConnectivity.Current.IsConnected;
                if (Status == false)
                {
                    var CurrentLoc = _CheckInCheckOutDB.GetCheckInOutLocation().ToList();
                    string JsonCurrentlocation2 = JsonConvert.SerializeObject(CurrentLoc);
                    tblUploadBookings objupload = new tblUploadBookings();
                    objupload.DateTime = DateTime.Now;
                    if (GlobalUserDetail.CheckInCheckOutType.ToString() == "CheckIn")
                    {
                        objupload.Direction = 0;
                        objupload.ReasonCode = 0;
                    }
                    else if (GlobalUserDetail.CheckInCheckOutType.ToString() == "Checkout")
                    {
                        objupload.Direction = 1;
                        objupload.ReasonCode = 0;
                    }
                    else if (GlobalUserDetail.CheckInCheckOutType.ToString() == "ReasonOut")
                    {
                        objupload.Direction = 2;
                        objupload.ReasonCode = GlobalUserDetail.CodeOut;
                    }
                    else if (GlobalUserDetail.CheckInCheckOutType.ToString() == "ReasonIn")
                    {
                        objupload.Direction = 2;
                        objupload.ReasonCodeIn = GlobalUserDetail.CodeIn;
                    }

                    objupload.Location = JsonCurrentlocation2;
                    _UploadBookingDB.AddUploadBookings(objupload);
                    GlobalUserDetail.CheckInOut = "Used";
                    await Navigation.PopAsync(true);

                }
                else
                {
                    await Navigation.PushPopupAsync(loadingPage);
                    var GetCacheBooking = _UploadBookingDB.GetUploadBookings().ToList();
                    if (GetCacheBooking.Count > 0)
                    {
                        foreach (var items in GetCacheBooking)
                        {
                            List<UploadBooking> objuploadbookingList1 = new List<UploadBooking>();
                            UploadBooking objuploadbooking = new UploadBooking();
                            objuploadbooking.DateTime = DateTime.Now;
                            if (GlobalUserDetail.CheckInCheckOutType.ToString() == "CheckIn")
                            {
                                objuploadbooking.Direction = 0;
                                objuploadbooking.ReasonCode = 0;
                            }
                            else if (GlobalUserDetail.CheckInCheckOutType.ToString() == "Checkout")
                            {
                                objuploadbooking.Direction = 1;
                                objuploadbooking.ReasonCode = 0;
                            }
                            else if (GlobalUserDetail.CheckInCheckOutType.ToString() == "ReasonOut")
                            {
                                objuploadbooking.Direction = 2;
                                objuploadbooking.ReasonCode = items.ReasonCode;
                            }
                            else if (GlobalUserDetail.CheckInCheckOutType.ToString() == "ReasonIn")
                            {
                                objuploadbooking.Direction = 2;
                                objuploadbooking.ReasonCode = items.ReasonCodeIn;
                            }

                            objuploadbooking.Location = items.Location;
                            objuploadbookingList1.Add(objuploadbooking);
                            using (var client = new HttpClient(new NativeMessageHandler()))
                            {

                                // Uri baseAddress = new Uri(GlobalUserDetail.ServerurlLink+ "/gatac_api/mobile" + "/" + GlobalUserDetail.GlobalGUID + "/Attendance");
                                Uri baseAddress = new Uri(GlobalUserDetail.ServerurlLink + GlobalUserDetail.GlobalGUID + "/Attendance");
                                client.BaseAddress = baseAddress;
                                var jsonobjectbooking = JsonConvert.SerializeObject(objuploadbookingList1);
                                var content = new StringContent(jsonobjectbooking, Encoding.UTF8, "application/json");
                                HttpResponseMessage response = null;
                                response = await client.PostAsync(baseAddress, content);
                                if (response.IsSuccessStatusCode)
                                {

                                }
                                else
                                {
                                    await DisplayAlert("", "Server response Failed", "OK");
                                }
                            }
                        }

                        // GlobalUserDetail.CheckInOut = "Used";
                        // await Navigation.PopAsync(true);
                        _UploadBookingDB.DeleteUploadBookings();
                        // await Navigation.RemovePopupPageAsync(loadingPage);
                    }
                    var GetCacheRecords = _UploadBookingDB.GetUploadBookings().ToList();
                    if (GetCacheRecords.Count() == 0)
                    {
                        var GetAsignedlocation = App.LocationList;
                        string json = JsonConvert.SerializeObject(GetAsignedlocation);
                        var Geocurrentlocation1 = objgeolocation;
                        string JsonCurrentlocation1 = JsonConvert.SerializeObject(Geocurrentlocation1);
                        List<UploadBooking> objuploadbookingList = new List<UploadBooking>();
                        UploadBooking objuploadbooking1 = new UploadBooking();
                        objuploadbooking1.DateTime = DateTime.Now;
                        if (GlobalUserDetail.CheckInCheckOutType.ToString() == "CheckIn")
                        {
                            objuploadbooking1.Direction = 0;
                           objuploadbooking1.ReasonCode = 0;
                        }
                        else if (GlobalUserDetail.CheckInCheckOutType.ToString() == "Checkout")
                        {
                            objuploadbooking1.Direction = 1;
                           objuploadbooking1.ReasonCode = 0;
                        }
                        else if (GlobalUserDetail.CheckInCheckOutType.ToString() == "ReasonOut")
                        {
                            objuploadbooking1.Direction = 2;
                           objuploadbooking1.ReasonCode = GlobalUserDetail.CodeOut;
                        }
                        else if (GlobalUserDetail.CheckInCheckOutType.ToString() == "ReasonIn")
                        {
                            objuploadbooking1.Direction = 2;
                            objuploadbooking1.ReasonCode = GlobalUserDetail.CodeIn;
                        }
                        objuploadbooking1.Location = JsonCurrentlocation1;
                        objuploadbookingList.Add(objuploadbooking1);
                        using (var client1 = new HttpClient(new NativeMessageHandler()))
                        {

                            //   // GlobalUserDetail.ServerurlLink + "/gatac_api/mobile"
                            //   // Uri baseAddress = new Uri(GlobalUserDetail.ServerurlLink+ "/gatac_api/mobile" + "/" + GlobalUserDetail.GlobalGUID + "/Attendance");
                            Uri baseAddress1 = new Uri(GlobalUserDetail.ServerurlLink + GlobalUserDetail.GlobalGUID + "/Attendance");
                            client1.BaseAddress = baseAddress1;
                            var jsonobjectbooking1 = JsonConvert.SerializeObject(objuploadbookingList);
                            var content1 = new StringContent(jsonobjectbooking1, Encoding.UTF8, "application/json");
                            HttpResponseMessage response1 = null;
                            response1 = await client1.PostAsync(baseAddress1, content1);
                            if (response1.IsSuccessStatusCode)
                            {
                                var Res = await response1.Content.ReadAsStringAsync();
                                GlobalUserDetail.CheckInOut = "Used";
                                await Navigation.PopAsync(true);
                                await Navigation.RemovePopupPageAsync(loadingPage);
                            }
                            else
                            {
                                await Navigation.RemovePopupPageAsync(loadingPage);
                                await DisplayAlert("", "Server response Failed", "OK");
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

        private async void BtnCancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
        protected override void OnDisappearing()
        {
            try
            {
                base.OnDisappearing();
                CrossGeolocator.Current.PositionChanged -= Current_PositionChanged;
                CrossGeolocator.Current.PositionError -= Current_PositionError;
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            }
            catch (Exception ex)
            {

            }
        }
    }
    
}

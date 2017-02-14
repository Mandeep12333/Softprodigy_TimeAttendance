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
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GantnerMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAttendencePage : ContentPage
    {
        public AttendenceDB Db = new AttendenceDB();
        public tblAttendence _tblAttende;
        internal readonly IMessageDialog messageDialog;
        public string GlobalFromDate = string.Empty;
        public string GlobalTodate = string.Empty;
        public ObservableCollection<EmployeeWeeklyReport> AttendenceList = new ObservableCollection<EmployeeWeeklyReport>();
        EmployeeWeeklyReport objweeklyreport;
        public class EmployeeWeeklyReport
        {
            public string Date { get; set; }
            public string TimeIn { get; set; }
            public string TimeOut { get; set; }
            public string Total { get; set; }
        }
        public MyAttendencePage()
        {
            InitializeComponent();
            //if (Device.OS == TargetPlatform.Android)
            //{
            //    DependencyService.Get<Isethasnavigationbar>().Show();
            //}
            NavigationPage.SetHasNavigationBar(this, false);
            bool Status = CrossConnectivity.Current.IsConnected;
            if (Status == false)
            {
                OfflineData();
                Constructeroffline();
            }

            else
            {
                ConstructerArgyments();

            }
            messageDialog = DependencyService.Get<IMessageDialog>();
            Title = GlobalUserDetail.CompanyName.ToString();
            SetLanguageCulture();
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
        public void SetLanguageCulture()
        {
            string langCode = GlobalLanguageCulture.LanguageCode.ToString();
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo("en");
            L10n.SetLocale(ci);
            AppResources.Culture = ci;
            if (langCode == "en")
            {
                lblmyattendence.Text = "My Attendence";
                lblfrom.Text = "From";
                lblto.Text = "To";
            }
            else
            {
                lblmyattendence.Text = "دوامي";
                lblfrom.Text = "من";
                lblto.Text = "إلى";
            }
        }

        public async void OfflineData()
        {
            var GetAttendeceReport = Db.GetAttendence().ToList();
            if (GetAttendeceReport.Count() > 0)
            {
                AttendenceList.Clear();

                foreach (var item in GetAttendeceReport)
                {
                    objweeklyreport = new EmployeeWeeklyReport();
                    // DateTime Datetime = Convert.ToDateTime(item.Date);
                    objweeklyreport.Date = item.Date; //Datetime.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                    objweeklyreport.TimeIn = item.TimeIn;
                    objweeklyreport.TimeOut = item.TimeOut;
                    objweeklyreport.Total = item.Total;
                    AttendenceList.Add(objweeklyreport);
                }
                dataGrid.IsVisible = true;
                dataGrid.ScrollingMode = ScrollingMode.PixelLine;
                dataGrid.ItemsSource = AttendenceList;
            }
            else
            {
                dataGrid.IsVisible = false;
                dataGrid.ScrollingMode = ScrollingMode.PixelLine;
                dataGrid.ItemsSource = AttendenceList;
                await DisplayAlert("", "No Record Found", "OK");
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void Constructeroffline()
        {
            try
            {
                FromDatepicker.Date = System.DateTime.Today;
                string dfrom = FromDatepicker.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                GlobalFromDate = dfrom;
                ToDatepicker.Date = System.DateTime.Today.AddDays(7);
                string Todate = ToDatepicker.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                GlobalTodate = Todate;
                FromDatepicker.DateSelected += FromDatepicker_DateSelected;
                ToDatepicker.DateSelected += ToDatepicker_DateSelected;
                if (Device.OS == TargetPlatform.Windows)
                {
                    NavigationPage.SetHasNavigationBar(this, false);
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage = new NavigationPage(new LoginPage("", "", "Used"))
                { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };
            }
        }

        public void ConstructerArgyments()
        {
            try
            {
                FromDatepicker.Date = System.DateTime.Today;
                string dfrom = FromDatepicker.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                GlobalFromDate = dfrom;
                ToDatepicker.Date = System.DateTime.Today.AddDays(7);
                string Todate = ToDatepicker.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                GlobalTodate = Todate;
                FromDatepicker.DateSelected += FromDatepicker_DateSelected;
                ToDatepicker.DateSelected += ToDatepicker_DateSelected;
                if (Device.OS == TargetPlatform.Windows)
                {
                    NavigationPage.SetHasNavigationBar(this, false);
                }
                GetMyAttendence();
            }
            catch (Exception ex)
            {
                App.Current.MainPage = new NavigationPage(new LoginPage("", "", "Used"))
                { Title = "Center", BarTextColor = Color.FromHex("#FFFFFF"), BarBackgroundColor = Color.FromHex("#2F6686") };
            }
        }

        public async void GetMyAttendence()
        {
            var loadingPage = new LoadingPopupPage();
            try
            {
                bool Status = CrossConnectivity.Current.IsConnected;
                if (Status == true)
                {
                    using (var client = new HttpClient(new NativeMessageHandler()))
                    {
                        await Navigation.PushPopupAsync(loadingPage);
                        // GlobalUserDetail.ServerurlLink + "/gatac_api/mobile
                        //var RestUrl = string.Format(GlobalUserDetail.ServerurlLink+ "/gatac_api/mobile" + "/" + GlobalUserDetail.GlobalGUID + "/Attendance" + "?fromDate=" + GlobalFromDate + "&toDate=" + GlobalTodate + "");
                        var RestUrl = string.Format(GlobalUserDetail.ServerurlLink + GlobalUserDetail.GlobalGUID + "/Attendance" + "?fromDate=" + GlobalFromDate + "&toDate=" + GlobalTodate + "");
                        client.BaseAddress = new Uri(RestUrl);
                        HttpResponseMessage response = await client.GetAsync(RestUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var GetobjProfile = JsonConvert.DeserializeObject<MyAttendence[]>(content);
                            if (GetobjProfile.Count() > 0)
                            {
                                Db.DeleteAttendence();
                                AttendenceList.Clear();
                                foreach (var item in GetobjProfile)
                                {
                                    if (item.isAbsent == false)
                                    {
                                        foreach (var items in item.shiftRecords)
                                        {
                                            objweeklyreport = new EmployeeWeeklyReport();
                                            _tblAttende = new tblAttendence();
                                            DateTime Datetime = Convert.ToDateTime(item.date);
                                            objweeklyreport.Date = Datetime.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                                            objweeklyreport.TimeIn = items.timeIn;
                                            objweeklyreport.TimeOut = items.timeOut;
                                            objweeklyreport.Total = items.totalHours;
                                            AttendenceList.Add(objweeklyreport);
                                            _tblAttende.Date = Datetime.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                                            _tblAttende.TimeIn = items.timeIn;
                                            _tblAttende.TimeOut = items.timeOut;
                                            _tblAttende.Total = items.totalHours;
                                            Db.AddAttendence(_tblAttende);

                                        }
                                    }
                                }
                                dataGrid.IsVisible = true;
                                dataGrid.ScrollingMode = ScrollingMode.PixelLine;
                                dataGrid.ItemsSource = AttendenceList;
                            }
                            else
                            {
                                await DisplayAlert("", "No Record Found", "OK");
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


        private async void ToDatepicker_DateSelected(object sender, DateChangedEventArgs e)
        {

            var loadingPage = new LoadingPopupPage();
            string ToDate = string.Empty;
            try
            {
                DateTime fromdate = FromDatepicker.Date;
                DateTime todate = ToDatepicker.Date;
                if (todate < fromdate)
                {
                    await DisplayAlert("End Date Error", "End date cannot be earlier than the start date, please try again", "OK");
                }
                else
                {
                    bool Status = CrossConnectivity.Current.IsConnected;
                    if (Status == true)
                    {
                        string dateTo = ToDatepicker.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                        //ToDate = date.ToString("yyyy-MM-dd");
                        //DateTime dt = DateTime.Parse(Todate1, CultureInfo.InvariantCulture);
                        ToDate = dateTo;
                        await Navigation.PushPopupAsync(loadingPage);
                        using (var client = new HttpClient(new NativeMessageHandler()))
                        {

                            //var RestUrl = string.Format(GlobalUserDetail.ServerurlLink+ "/gatac_api/mobile" + "/" + GlobalUserDetail.GlobalGUID + "/Attendance" + "?fromDate=" + GlobalFromDate + "&toDate=" + ToDate + "");
                            var RestUrl = string.Format(GlobalUserDetail.ServerurlLink + GlobalUserDetail.GlobalGUID + "/Attendance" + "?fromDate=" + GlobalFromDate + "&toDate=" + ToDate + "");
                            client.BaseAddress = new Uri(RestUrl);
                            HttpResponseMessage response = await client.GetAsync(RestUrl);
                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                var GetobjProfile = JsonConvert.DeserializeObject<MyAttendence[]>(content);
                                if (GetobjProfile.Count() > 0)
                                {
                                    Db.DeleteAttendence();
                                    AttendenceList.Clear();
                                    foreach (var item in GetobjProfile)
                                    {
                                        if (item.isAbsent == false)
                                        {
                                            foreach (var items in item.shiftRecords)
                                            {
                                                objweeklyreport = new EmployeeWeeklyReport();
                                                _tblAttende = new tblAttendence();
                                                DateTime Datetime = Convert.ToDateTime(item.date);
                                                objweeklyreport.Date = Datetime.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                                                objweeklyreport.TimeIn = items.timeIn;
                                                objweeklyreport.TimeOut = items.timeOut;
                                                objweeklyreport.Total = items.totalHours;
                                                AttendenceList.Add(objweeklyreport);
                                                _tblAttende.Date = Datetime.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                                                _tblAttende.TimeIn = items.timeIn;
                                                _tblAttende.TimeOut = items.timeOut;
                                                _tblAttende.Total = items.totalHours;
                                                Db.AddAttendence(_tblAttende);
                                            }
                                        }
                                    }
                                    dataGrid.IsVisible = true;
                                    dataGrid.ScrollingMode = ScrollingMode.PixelLine;
                                    dataGrid.ItemsSource = AttendenceList;
                                }
                                await Navigation.RemovePopupPageAsync(loadingPage);

                            }
                            else
                            {
                                await Navigation.RemovePopupPageAsync(loadingPage);
                                await DisplayAlert("Error", "Failed to connect to server", "OK");
                            }

                        }

                    }
                    else
                    {
                        var Langcode = CrossSecureStorage.Current.GetValue("Langcode");
                        if (Langcode != null)
                        {
                            OfflineData();
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

        private async void FromDatepicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            DateTime fromdate = FromDatepicker.Date;
            string dfrom = FromDatepicker.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            GlobalFromDate = dfrom;
            DateTime fromdate1 = FromDatepicker.Date;
            DateTime todate = ToDatepicker.Date;
            if (fromdate > todate)
            {
                await DisplayAlert("", "Please select Lessthan date from to date!", "OK");
            }
        }


        protected override void OnDisappearing()
        {

            base.OnDisappearing();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }
        //public async void GenerateReport()
        //{

        //    data = new ObservableCollection<EmployeeWeeklyReport>();
        //    data.Add(new EmployeeWeeklyReport { Date = "9/11/2016", TimeIn = "7:00 AM", TimeOut = "8:00 PM", Total = "08" });
        //    data.Add(new EmployeeWeeklyReport { Date = "16/11/2016", TimeIn = "7:00 AM", TimeOut = "8:00 PM", Total = "08" });
        //    data.Add(new EmployeeWeeklyReport { Date = "17/11/2016", TimeIn = "7:00 AM", TimeOut = "8:00 PM", Total = "08" });
        //    data.Add(new EmployeeWeeklyReport { Date = "18/11/2016", TimeIn = "7:00 AM", TimeOut = "8:00 PM", Total = "08" });
        //    data.Add(new EmployeeWeeklyReport { Date = "19/11/2016", TimeIn = "7:00 AM", TimeOut = "8:00 PM", Total = "08" });
        //    data.Add(new EmployeeWeeklyReport { Date = "20/11/2016", TimeIn = "7:00 AM", TimeOut = "8:00 PM", Total = "08" });
        //    data.Add(new EmployeeWeeklyReport { Date = "12/11/2016", TimeIn = "7:00 AM", TimeOut = "8:00 PM", Total = "08" });


        //}
    }
}

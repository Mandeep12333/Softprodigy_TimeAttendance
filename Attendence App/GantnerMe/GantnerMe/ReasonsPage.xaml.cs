using GantnerMe.Class;
using GantnerMe.Interface;
using GantnerMe.ViewModel;
using Plugin.Connectivity;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
    public partial class ReasonsPage : PopupPage
    {
        internal readonly IMessageDialog messageDialog;
        public ReasonDB _Reasondb = new ReasonDB();
        List<ClsReason> List = new List<ClsReason>();
        public ReasonsPage()
        {
            InitializeComponent();
            bool Status = CrossConnectivity.Current.IsConnected;
            if(Status==false)
            {
                GetOnlineOfflineReason();
            }
            else
            {
                GetOnlineOfflineReason();
            }
            string Lang = GlobalLanguageCulture.LanguageCode;
            if(Lang== "ar")
            {
                lblreason.Text = "أسباب";
            }
            else
            {
                lblreason.Text = "Reasons";
            }

            messageDialog = DependencyService.Get<IMessageDialog>();
            ReasonsListView.ItemTapped += ReasonsListView_ItemTapped;
            ReasonsListView.ItemSelected += ReasonsListView_ItemSelected;
            
        }

        private void ReasonsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ReasonsListView.SelectedItem = null;
        }

        private void ReasonsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            ((ListView)sender).SelectedItem = null;
        }

        public  void GetOnlineOfflineReason()
        {
            var GetAllreasons = _Reasondb.GetReasons().ToList();
            if (GetAllreasons.Count() > 0)
            {
                foreach (var item in GetAllreasons)
                {
                    ClsReason objreasons = new ClsReason();
                    objreasons.CodeIn = item.codeIn.ToString();
                    string Lang = GlobalLanguageCulture.LanguageCode;
                    if (Lang == "ar")
                    {
                        objreasons.ReasonNameIn = "في" + " " + item.name.ToString();
                        objreasons.CodeOut = item.codeOut.ToString();
                        objreasons.ReasonNameOut = "خارج" + " " + item.name.ToString();
                    }
                    else
                    {
                        objreasons.ReasonNameIn = item.name.ToString() + " in";
                        objreasons.CodeOut = item.codeOut.ToString();
                        objreasons.ReasonNameOut = item.name.ToString() + " out";
                    }
                    List.Add(objreasons);
                    //ClsReason objreasons2 = new ClsReason();
                    //objreasons2.CodeOut = item.codeOut.ToString();
                    //objreasons2.ReasonName = item.name.ToString() + " out";
                    //List.Add(objreasons2);
                }
                ReasonsListView.ItemsSource = List;
            }
        }

        public async void GetAllreasons()
        {
            
           
           // ClsReason objreasons;
            try
            {
                var Reasons = App.GetReasonlist;
                // ReasonsListView.ItemsSource = Reasons;
                if (Reasons.Count > 0)
                {
                    foreach (var item in Reasons)
                    {
                        ClsReason objreasons = new ClsReason();
                        objreasons.CodeIn = item.codeIn.ToString();
                        objreasons.ReasonNameIn = item.name.ToString() + " in";
                        objreasons.CodeOut = item.codeOut.ToString();
                        objreasons.ReasonNameOut = item.name.ToString() + " out";
                        List.Add(objreasons);

                        //ClsReason objreasons2 = new ClsReason();
                        //objreasons2.CodeOut = item.codeOut.ToString();
                        //objreasons2.ReasonName = item.name.ToString() + " out";
                        //List.Add(objreasons2);
                    }

                    ReasonsListView.ItemsSource = List;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                await DisplayAlert("Server error", "", "OK");
            }
        }

        public async void OnTapGestureReasonINTapped(object sender, EventArgs args)
        {
            dynamic dyaniccodein = (Xamarin.Forms.Button)sender;
            var CodeIN = dyaniccodein.CommandParameter;
            var CIN = Convert.ToInt32(CodeIN);
            GlobalUserDetail.CodeIn = CIN;
            GetPermisionReasonIn();

        }
        public async void OnTapGestureReasonOutTapped(object sender, EventArgs args)
        {
            dynamic dyaniccodout = (Xamarin.Forms.Button)sender;
            var CodeIN = dyaniccodout.CommandParameter;
            var CIOut = Convert.ToInt32(CodeIN);
            GlobalUserDetail.CodeOut = CIOut;
            GetPermisionReasonOut();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;
            //OfficialIn.Scale = 0.3;
            //OfficialIn.Opacity = 0;
            //LunchOut.Scale = 0.3;
            //LunchOut.Opacity = 0;
            //LunchIn.Scale = 0.3;
            //LunchIn.Opacity = 0;
        }
        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();
            return false;
        }
        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }

        protected override void OnDisappearing()
        {
            Content = null;
            base.OnDisappearing();
            GC.Collect();
        }

        protected async override Task OnAppearingAnimationEnd()
        {
            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0));
            //OfficialIn.ScaleTo(1),
            //OfficialIn.FadeTo(1),
            //LunchOut.ScaleTo(1),
            //LunchOut.FadeTo(1),
            //LunchIn.ScaleTo(1),
            //LunchIn.FadeTo(1));
        }

        //protected async override Task OnDisappearingAnimationBegin()
        //{
        //    var taskSource = new TaskCompletionSource<bool>();

        //    var currentHeight = FrameContainer.Height;

        //    FrameContainer.Animate("HideAnimation", d =>
        //    {
        //        FrameContainer.HeightRequest = d;
        //    },
        //    start: currentHeight,
        //    end: 170,
        //    finished: async (d, b) =>
        //    {
        //        await Task.Delay(300);
        //        taskSource.TrySetResult(true);
        //    });

        //    await taskSource.Task;
        //}

        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        public async void ReasonOutRedirectMethod()
        {
            CloseAllPopup();
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            NavigationPage NP = ((NavigationPage)((MasterDetailPage)App.Current.MainPage).Detail);
            await NP.PushAsync(new CheckInCheckOutMapPage("ReasonOut"), true);
            //await Navigation.PushAsync(new CheckInCheckOutMapPage("LunchOut"), true);
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        public async void ReasonInRedirectMethod()
        {
            CloseAllPopup();
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            NavigationPage NP = ((NavigationPage)((MasterDetailPage)App.Current.MainPage).Detail);
            await NP.PushAsync(new CheckInCheckOutMapPage("ReasonIn"), true);
            // await Navigation.PushAsync(new CheckInCheckOutMapPage("LunchIn"), true);
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

       

        public async void GetPermisionReasonOut()
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
                            ReasonOutRedirectMethod();
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
                        ReasonOutRedirectMethod();
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

        public async void GetPermisionReasonIn()
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
                            ReasonInRedirectMethod();
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
                        ReasonInRedirectMethod();
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

    }
}

using GantnerMe.Interface;
using GantnerMe.Resx;
using GantnerMe.ViewModel;
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
    public partial class LocationAlertPage : PopupPage
    {
        internal readonly IMessageDialog messageDialog;
        public LocationAlertPage()
        {
            InitializeComponent();
            messageDialog = DependencyService.Get<IMessageDialog>();
            SetLanguageCulture();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FrameContainer.HeightRequest = -1;

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;
            Nobutton.Scale = 0.3;
            Nobutton.Opacity = 0;
            YesButton.Scale = 0.3;
            YesButton.Scale = 0;

        }

        public void SetLanguageCulture()
        {
            string langCode = GlobalLanguageCulture.LanguageCode.ToString();
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo(langCode);
            L10n.SetLocale(ci);
            AppResources.Culture = ci;
            lblneedto.Text = AppResources.areyousureyouneedto;
            lblcuurentlocation.Text = AppResources.sendyourcurentlocation;
            lbltrsnlocation.Text = AppResources.transactionlocation;
            Nobutton.Text = AppResources.no;
            YesButton.Text = AppResources.yes;
            lblalert.Text = AppResources.alert;

        }

        protected async override Task OnAppearingAnimationEnd()
        {
            var translateLength = 400u;

            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0),
                Nobutton.ScaleTo(1),
                Nobutton.FadeTo(1),
            YesButton.ScaleTo(1),
             YesButton.FadeTo(1));
        }

        protected async override Task OnDisappearingAnimationBegin()
        {
            var taskSource = new TaskCompletionSource<bool>();

            var currentHeight = FrameContainer.Height;

            await Task.WhenAll(
                Nobutton.FadeTo(0),
            YesButton.FadeTo(0));
            FrameContainer.Animate("HideAnimation", d =>
            {
                FrameContainer.HeightRequest = d;
            },
            start: currentHeight,
            end: 170,
            finished: async (d, b) =>
            {
                await Task.Delay(300);
                taskSource.TrySetResult(true);
            });

            await taskSource.Task;
        }

        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.RemovePopupPageAsync(this);
        }

        public async void Btnyesclick(object sender, EventArgs e)
        {

            GetPermision();
        }

        public async void LocationisOn()
        {
            CloseAllPopup();
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            //await Navigation.PushAsync(new CheckInCheckOutMapPage("CheckIn"), true);
            NavigationPage NP = ((NavigationPage)((MasterDetailPage)App.Current.MainPage).Detail);
            await NP.PushAsync(new CheckInCheckOutMapPage("CheckIn"), true);
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        private async void BtnNoclick(object sender, EventArgs e)
        {
            await Navigation.PopAllPopupAsync();
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

    }
}

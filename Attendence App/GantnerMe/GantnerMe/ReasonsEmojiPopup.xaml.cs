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
    public partial class ReasonsEmojiPopup : PopupPage
    {
        internal readonly IMessageDialog messageDialog;
        public ReasonsEmojiPopup()
        {
            InitializeComponent();
            messageDialog = DependencyService.Get<IMessageDialog>();
            SetLanguageCulture();
        }

        public void SetLanguageCulture()
        {
            string langCode = GlobalLanguageCulture.LanguageCode.ToString();
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo(langCode);
            L10n.SetLocale(ci);
            AppResources.Culture = ci;
            lblhowwasurday.Text = AppResources.howwasyourday;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //FrameContainer.HeightRequest = -1;

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;

            //EditSaveButton.Scale = 0.3;
            //EditSaveButton.Opacity = 0;
            //CancelButton.Scale = 0.3;
            //CancelButton.Scale = 0;
            //UsernameEntry.TranslationX = -10;
            //UsernameEntry.Opacity = 0;
        }

        protected async override Task OnAppearingAnimationEnd()
        {
            var translateLength = 400u;
            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0));
                
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
        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }
        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }
        private async void OnTapGestureHappyclick(object sender, EventArgs e)
        {
            imghappy.Opacity = .5;
            await Task.Delay(100);
            imghappy.Opacity = 1;
            GetPermisionHappy1();


        }
        private async void OnTapGestureSadclick(object sender, EventArgs e)
        {
            imgconfused.Opacity = .5;
            await Task.Delay(100);
            imgconfused.Opacity = 1;
            GetPermisionSad();


        }
        private async void OnTapGestureHappyclick2(object sender, EventArgs e)
        {
            imgsad.Opacity = .5;
            await Task.Delay(100);
            imgsad.Opacity = 1;
            GetPermisionHappy2();

        }


        public async void HappyclickredirectMethod()
        {
            CloseAllPopup();
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            NavigationPage NP = ((NavigationPage)((MasterDetailPage)App.Current.MainPage).Detail);
            await NP.PushAsync(new CheckInCheckOutMapPage("Checkout"), true);
            // await Navigation.PushAsync(new CheckInCheckOutMapPage("Checkout"), true);
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        public async void SadRedirectMethod()
        {
            CloseAllPopup();
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            NavigationPage NP = ((NavigationPage)((MasterDetailPage)App.Current.MainPage).Detail);
            await NP.PushAsync(new CheckInCheckOutMapPage("Checkout"), true);
            // await Navigation.PushAsync(new CheckInCheckOutMapPage("Checkout"), true);
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        public async void happy2RedirectMethod()
        {
            CloseAllPopup();
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            NavigationPage NP = ((NavigationPage)((MasterDetailPage)App.Current.MainPage).Detail);
            await NP.PushAsync(new CheckInCheckOutMapPage("Checkout"), true);
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        public async void GetPermisionHappy1()
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
                            HappyclickredirectMethod();
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
                        HappyclickredirectMethod();
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

        public async void GetPermisionSad()
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
                            SadRedirectMethod();
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
                        SadRedirectMethod();
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

        public async void GetPermisionHappy2()
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
                            happy2RedirectMethod();
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
                        happy2RedirectMethod();
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

        protected override void OnDisappearing()
        {
            Content = null;
            base.OnDisappearing();
            GC.Collect();
        }


    }
}

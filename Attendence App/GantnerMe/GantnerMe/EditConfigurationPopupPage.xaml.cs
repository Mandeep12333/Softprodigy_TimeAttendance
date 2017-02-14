using GantnerMe.Helper;
using GantnerMe.Interface;
using GantnerMe.Resx;
using GantnerMe.ViewModel;
using ModernHttpClient;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.SecureStorage;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
    public partial class EditConfigurationPopupPage : PopupPage
    {
        internal readonly IMessageDialog messageDialog;
        public EditConfigurationPopupPage()
        {
            InitializeComponent();
            if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Windows)
            {
                boxviewconfigurl.IsVisible = false;
            }
            SetLanguageCulture();
            messageDialog = DependencyService.Get<IMessageDialog>();
            var ConfigLink = CrossSecureStorage.Current.GetValue("Url");
            Configlinkurl.Text = ConfigLink;
        }

        public void SetLanguageCulture()
        {
            string langCode = GlobalLanguageCulture.LanguageCode.ToString();
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo(langCode);
            L10n.SetLocale(ci);
            AppResources.Culture = ci;
            lbleditconfig.Text = AppResources.editconfigurations;
            EditSaveButton.Text = AppResources.save;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //FrameContainer.HeightRequest = -1;

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;

            EditSaveButton.Scale = 0.3;
            EditSaveButton.Opacity = 0;
            Configlinkurl.TranslationX = -10;
            Configlinkurl.Opacity = 0;
        }

        protected async override Task OnAppearingAnimationEnd()
        {
            var translateLength = 400u;

            await Task.WhenAll(
                Configlinkurl.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                Configlinkurl.FadeTo(1),
                (new Func<Task>(async () =>
                {
                    await Task.Delay(200);
                }))());

            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0),
                EditSaveButton.ScaleTo(1),
                EditSaveButton.FadeTo(1));

        }

        //protected async override Task OnDisappearingAnimationBegin()
        //{
        //    var taskSource = new TaskCompletionSource<bool>();

        //    var currentHeight = FrameContainer.Height;

        //    await Task.WhenAll(
        //        UsernameEntry.FadeTo(0),
        //        EditSaveButton.FadeTo(0));
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

        private async void OnSaveclick(object sender, EventArgs e)
        {
            string serverlink = Configlinkurl.Text;
            if (string.IsNullOrWhiteSpace(serverlink))
            {
                if (Device.OS == TargetPlatform.Windows)
                {
                    await DisplayAlert("", AppResources.pleaseenterserverlink, "OK");
                }
                else
                {
                    messageDialog.SendToast(AppResources.pleaseenterserverlink);
                }

            }
            else if (!string.IsNullOrWhiteSpace(serverlink))
            {
                if (Uri.IsWellFormedUriString(serverlink, UriKind.Absolute))
                {

                    var loadingPage = new LoadingPopupPage();
                    await Navigation.PushPopupAsync(loadingPage);
                    await Task.Delay(1000);
                    CrossSecureStorage.Current.DeleteKey("Url");
                    CrossSecureStorage.Current.SetValue("Url", serverlink);
                    GlobalUserDetail.ServerurlLink = serverlink;
                    if (Device.OS == TargetPlatform.Windows)
                    {
                        await DisplayAlert("", AppResources.Configurlupdated, "OK");
                    }
                    else
                    {
                        messageDialog.SendToast(AppResources.Configurlupdated);
                    }
                    CloseAllPopup();
                    await Navigation.RemovePopupPageAsync(loadingPage);

                }
                else
                {
                    if (Device.OS == TargetPlatform.Windows)
                    {
                        await DisplayAlert("", AppResources.pleaseentervalidserverlink, "OK");
                    }
                    else {
                        messageDialog.SendToast(AppResources.pleaseentervalidserverlink);
                    }
                    Configlinkurl.Text = string.Empty;
                }
            }
            
        }

    }
}


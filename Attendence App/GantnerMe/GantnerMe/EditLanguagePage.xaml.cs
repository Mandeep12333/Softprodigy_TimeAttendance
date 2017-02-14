using GantnerMe.Interface;
using GantnerMe.Resx;
using GantnerMe.ViewModel;
using Plugin.SecureStorage;
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
    public partial class EditLanguagePage : PopupPage
    {
        internal readonly IMessageDialog messageDialog;
        public class AppLanguage
        {
            public string LanguageText { get; set; }
        }
        public EditLanguagePage()
        {
            InitializeComponent();
            SetLanguageCulture();
            messageDialog = DependencyService.Get<IMessageDialog>();
            bindpicker();
           // LanguagePicker.SelectedIndexChanged += LanguagePicker_SelectedIndexChanged;
        }

        public void SetLanguageCulture()
        {
            string langCode = GlobalLanguageCulture.LanguageCode.ToString();
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo(langCode);
            L10n.SetLocale(ci);
            AppResources.Culture = ci;
            lblselectlanguage.Text = AppResources.selectyourlanguage;
            EditSaveButton.Text = AppResources.save;
        }

        //private void LanguagePicker_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (LanguagePicker.SelectedIndex == -1)
        //    {

        //    }
        //    else
        //    {
        //        string SelectedLanguage = LanguagePicker.Items[LanguagePicker.SelectedIndex];
        //        if (SelectedLanguage == "English")
        //        {
        //            GlobalLanguageCulture.LanguageCode = "en";
        //        }
        //        else
        //        {
        //            GlobalLanguageCulture.LanguageCode = "ar";
        //        }
        //    }
        //}

        public void bindpicker()
        {
            string Language = Convert.ToString(GlobalLanguageCulture.SelectedLang);
            if (Language == "English")
            {
                imageEnglish.IsVisible = true;
                imageArabic.IsVisible = false;
                GlobalLanguageCulture.LanguageCode = "en";
            }
            else
            {
                imageEnglish.IsVisible = false;
                imageArabic.IsVisible = true;
                GlobalLanguageCulture.LanguageCode = "ar";

            }

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
            imageEnglish.TranslationX = -10;
            imageEnglish.Opacity = 0;
            imageArabic.TranslationX = -10;
            imageArabic.Opacity = 0;
           // lblenglish.TranslationX = -10;
           // lblenglish.Opacity = 0;
            
        }

        protected async override Task OnAppearingAnimationEnd()
        {
            var translateLength = 400u;

            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0),

                imageArabic.FadeTo(1),
                imageArabic.ScaleTo(1, easing: Easing.SpringOut),
                imageArabic.RotateTo(0),

                imageEnglish.FadeTo(1),
                imageEnglish.ScaleTo(1, easing: Easing.SpringOut),
                imageEnglish.RotateTo(0),
                EditSaveButton.ScaleTo(1),
                EditSaveButton.FadeTo(1));

        }

        //protected async override Task OnDisappearingAnimationBegin()
        //{
        //    var taskSource = new TaskCompletionSource<bool>();

        //    var currentHeight = FrameContainer.Height;

        //    await Task.WhenAll(
        //        lblenglish.FadeTo(0),
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

        private  async void OnEditLanguage(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            await Task.Delay(2000);
            CloseAllPopup();
            string langCode = GlobalLanguageCulture.LanguageCode.ToString();
            CrossSecureStorage.Current.DeleteKey("Langcode");
            CrossSecureStorage.Current.SetValue("Langcode", langCode);
            if (langCode=="en")
            {
                GlobalLanguageCulture.SelectedLang = "English";
            }
            else
            {
                GlobalLanguageCulture.SelectedLang = "العربية";
            }

            GlobalUserDetail.EditLanguage = "Used";
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo(langCode);
            L10n.SetLocale(ci);
            AppResources.Culture = ci;
            await Navigation.RemovePopupPageAsync(loadingPage);
            CloseAllPopup();
             
            NavigationPage NP = ((NavigationPage)((MasterDetailPage)App.Current.MainPage).Detail);
            await NP.Navigation.PopAsync();
           // await NP.Navigation.PushAsync(new TASystemTabbedPage(), true);
            //await NP.Navigation.PopAsync();
            // await Navigation.PushAsync(new TASystemTabbedPage(), true);
            if (Device.OS == TargetPlatform.Windows)
            {
                await DisplayAlert("",AppResources.LanguageUpdated, "OK");
            }
            else
            {
                messageDialog.SendToast(AppResources.LanguageUpdated);
            }
        }

        public void BtnEnglishClick(object sender, EventArgs e)
        {
            GlobalLanguageCulture.LanguageCode = "en";
            CrossSecureStorage.Current.SetValue("Langcode", GlobalLanguageCulture.LanguageCode);
            //var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo("en");
            //L10n.SetLocale(ci);
            //AppResources.Culture = ci;
            GlobalLanguageCulture.SelectedLang = "English";
            imageEnglish.IsVisible = true;
            imageArabic.IsVisible = false;
        }

        public void BtnArabicClick(object sender, EventArgs e)
        {
            GlobalLanguageCulture.LanguageCode = "ar";
            CrossSecureStorage.Current.SetValue("Langcode", GlobalLanguageCulture.LanguageCode);
            //var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo("ar");
            //L10n.SetLocale(ci);
            //AppResources.Culture = ci;
            GlobalLanguageCulture.SelectedLang = "العربية";
            imageEnglish.IsVisible = false;
            imageArabic.IsVisible = true;
        }
    }
}

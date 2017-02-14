using GantnerMe.CustomControls;
using GantnerMe.Interface;
using GantnerMe.Resx;
using GantnerMe.ViewModel;
using Plugin.SecureStorage;
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
    public partial class SelectLanguagePage : ContentPage
    {
        public class AppLanguage
        {
            public string LanguageText { get; set; }
        }
        public SelectLanguagePage()
        {
            InitializeComponent();
            SetLanguageCulture();
            NavigationPage.SetHasNavigationBar(this, false);
            bindpicker();
            // LanguagePicker.SelectedIndexChanged += LanguagePicker_SelectedIndexChanged;
            // RadioButtonSelectLanguage.CheckedChanged += RadioButtonSelectLanguage_CheckedChanged;

        }
        public void BtnEnglishClick(object sender, EventArgs e)
        {
            GlobalLanguageCulture.LanguageCode = "en";
            CrossSecureStorage.Current.SetValue("Langcode", GlobalLanguageCulture.LanguageCode);
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo("en");
            L10n.SetLocale(ci);
            AppResources.Culture = ci;
            GlobalLanguageCulture.SelectedLang = "English";
            lblselectyourlang.Text = AppResources.selectyourlanguage;
            lblarabic.Opacity = .5;
            lblenglish.Opacity = 1;
            imageEnglish.IsVisible = true;
            imageArabic.IsVisible = false;
        }
        public void BtnArabicClick(object sender, EventArgs e)
        {
            GlobalLanguageCulture.LanguageCode = "ar";
            CrossSecureStorage.Current.SetValue("Langcode", GlobalLanguageCulture.LanguageCode);
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo("ar");
            L10n.SetLocale(ci);
            AppResources.Culture = ci;
            GlobalLanguageCulture.SelectedLang = "العربية";
            lblselectyourlang.Text = AppResources.selectyourlanguage;
            imageEnglish.IsVisible = false;
            lblarabic.Opacity = 1;
            lblenglish.Opacity = .5;
            imageArabic.IsVisible = true;
        }
        public void SetLanguageCulture()
        {
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo("en");
            L10n.SetLocale(ci);
            AppResources.Culture = ci;
            lblselectyourlang.Text = AppResources.selectyourlanguage;
            // btnlanguagesubmit.Text = AppResources.Next;
        }

        public void bindpicker()
        {
            //List<AppLanguage> list = new List<AppLanguage>();
            //list.Add(new AppLanguage() { LanguageText = "English" });
            //list.Add(new AppLanguage() { LanguageText = "العربية" });
            //foreach(var items in list)
            //{
            //    string language = items.LanguageText;
            //    LanguagePicker.Items.Add(language);
            //}
            //LanguagePicker.SelectedIndex = 0;
            GlobalLanguageCulture.LanguageCode = "en";
            GlobalLanguageCulture.SelectedLang = "English";
            CrossSecureStorage.Current.SetValue("Langcode", GlobalLanguageCulture.LanguageCode);
        }

        protected override void OnAppearing()
        {

        }

        //private void LanguagePicker_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (LanguagePicker.SelectedIndex == -1)
        //    {

        //    }
        //    else
        //    {
        //        string SelectedLanguage = LanguagePicker.Items[LanguagePicker.SelectedIndex];
        //        if(SelectedLanguage== "English")
        //        {
        //            GlobalLanguageCulture.LanguageCode = "en";
        //            CrossSecureStorage.Current.SetValue("Langcode", GlobalLanguageCulture.LanguageCode);
        //            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo("en");
        //            L10n.SetLocale(ci);
        //            AppResources.Culture = ci;
        //            lblselectyourlang.Text = AppResources.selectyourlanguage;
        //           // btnlanguagesubmit.Text = AppResources.Next;
        //            GlobalLanguageCulture.SelectedLang = "English";
        //        }
        //        else
        //        {
        //            GlobalLanguageCulture.LanguageCode = "ar";
        //            CrossSecureStorage.Current.SetValue("Langcode", GlobalLanguageCulture.LanguageCode);
        //            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo("ar");
        //            L10n.SetLocale(ci);
        //            AppResources.Culture = ci;
        //            lblselectyourlang.Text = AppResources.selectyourlanguage;
        //           // btnlanguagesubmit.Text = AppResources.Next;
        //            GlobalLanguageCulture.SelectedLang = "العربية";
        //        }
        //    }
        //}

        private void RadioButtonSelectLanguage_CheckedChanged(object sender, int e)
        {
            var radio = sender as CustomRadioButton;

            if (radio == null || radio.Id == -1) return;

            string Selectext = radio.Text;
        }

        public async void btnlanguageselectclick(object sender, EventArgs e)
        {
            // btnlanguagesubmit.IsEnabled = false;
            btnlanguagesubmit.Opacity = .5;
            btnlanguagesubmit.Opacity = 1;
            await Navigation.PushAsync(new ServerLinkPage(), true);
            // btnlanguagesubmit.IsEnabled = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

        }
    }
}

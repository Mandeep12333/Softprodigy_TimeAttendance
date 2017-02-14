using GantnerMe.Interface;
using GantnerMe.ViewModel;
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
    public partial class InfoPage : ContentPage
    {
        public InfoPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //if (Device.OS == TargetPlatform.Android)
            //{
            //    DependencyService.Get<Isethasnavigationbar>().Show();
            //}
            Title = GlobalUserDetail.CompanyName.ToString();
            
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
    }
}

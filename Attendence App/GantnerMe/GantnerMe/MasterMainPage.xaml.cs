using GantnerMe.Extensions;
using GantnerMe.ViewModel;
using Rg.Plugins.Popup.Extensions;
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
    public partial class MasterMainPage : MasterDetailPage
    {
        public MasterMainPage()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;
            NavigationPage.SetHasNavigationBar(this, false);
            // Master = masterPage;
            //  Detail= new NavigationPage(new MainPage());
            masterPage.ListView.ItemSelected += OnItemSelected;
            masterPage.ListView.ItemTapped += ListView_ItemTapped;
            // MessagingCenter.Send<MasterMainPage>(this, "Hi");
        }



        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //MessagingCenter.Subscribe<MainPage>(this, "Hi", (sender) => {
            //    IsPresented = true;
            //    // do something whenever the "Hi" message is sent
            //});

        }

        public void ismasterpageinstamce()
        {
            IsPresented = true;
        }

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var adv = new ActivityIndicator();
                adv.IsRunning = true;
                adv.Color = Color.Red;
                var item = e.SelectedItem as MasterPageItem;
                masterPage.ListView.SelectedItem = null;
                if (item != null)
                {
                    if (item.Title.ToString() == "Home")
                    {
                        var loadingPage = new LoadingPopupPage();
                        await Navigation.PushPopupAsync(loadingPage);
                        await App.Sleep(500);
                        Page displaypage = (Page)Activator.CreateInstance(item.TargetType);
                        NavigationPage page = new NavigationPage(displaypage);
                        page.BarBackgroundColor = Color.FromHex("#2F6686");
                        page.BarTextColor = Color.FromHex("#FFFFFF");
                        Detail = page;
                        masterPage.ListView.SelectedItem = null;
                        IsPresented = false;
                        await Navigation.RemovePopupPageAsync(loadingPage);

                    }
                    else if (item.Title.ToString() == "User Profile")
                    {
                        var loadingPage = new LoadingPopupPage();
                        await Navigation.PushPopupAsync(loadingPage);
                        await App.Sleep(500);
                       // GlobalUserDetail.UserProfile = "Used";
                       // Page displaypage = (Page)Activator.CreateInstance(item.TargetType);
                       // NavigationPage page = new NavigationPage(displaypage);
                       // page.BarBackgroundColor = Color.FromHex("#2F6686");
                       // page.BarTextColor = Color.FromHex("#FFFFFF");
                        masterPage.ListView.SelectedItem = null;
                        IsPresented = false;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Detail.Navigation.PushAsync(new UserProfilePage(), true);
                            await Navigation.RemovePopupPageAsync(loadingPage);
                        });
                    }
                    else if (item.Title.ToString() == "Info")
                    {
                        var loadingPage = new LoadingPopupPage();
                        await Navigation.PushPopupAsync(loadingPage);
                        await App.Sleep(500);
                        //GlobalUserDetail.Info = "Used";
                        //Page displaypage = (Page)Activator.CreateInstance(item.TargetType);
                        //NavigationPage page = new NavigationPage(displaypage);
                        //page.BarBackgroundColor = Color.FromHex("#2F6686");
                        //page.BarTextColor = Color.FromHex("#FFFFFF");
                        masterPage.ListView.SelectedItem = null;
                        IsPresented = false;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Detail.Navigation.PushAsync(new InfoPage(), true);
                            await Navigation.RemovePopupPageAsync(loadingPage);
                        });

                    }
                    else if (item.Title.ToString() == "Settings")
                    {
                        var loadingPage = new LoadingPopupPage();
                        await Navigation.PushPopupAsync(loadingPage);
                        await App.Sleep(500);
                       // Page displaypage = (Page)Activator.CreateInstance(item.TargetType);
                        //NavigationPage page = new NavigationPage(displaypage);
                        //page.BarBackgroundColor = Color.FromHex("#2F6686");
                       // page.BarTextColor = Color.FromHex("#FFFFFF");
                        // Detail = page;
                        masterPage.ListView.SelectedItem = null;
                        IsPresented = false;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Detail.Navigation.PushAsync(new SettingPage(), true);
                            await Navigation.RemovePopupPageAsync(loadingPage);
                        });

                    }
                }
            }
            catch (Exception EX)
            {

            }
        }

    }
}

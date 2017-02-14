using GantnerMe.Extensions;
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
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } }
        public MasterPage()
        {
            InitializeComponent();
            this.Title= "Menu";
            this.Icon = "menu_ic.png";
           // listView.ItemSelected += ListView_ItemSelected;
            NavigationPage.SetHasNavigationBar(this, false);
            var masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Home",
                IconSource = "home_ic_off_canvas.png",
                Rightarrow = "right_arrow_ic.png",
                TargetType = typeof(MainPage)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "User Profile",
                IconSource = "user_profile_ic_menu.png",
                Rightarrow = "right_arrow_ic.png",
                
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Info",
                IconSource = "info_ic_menu.png",
                Rightarrow = "right_arrow_ic.png",

            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Settings",
                IconSource = "settings_ic_menu.png",
                Rightarrow = "right_arrow_ic.png",
               
            });
            listView.ItemsSource = masterPageItems;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        //private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as MasterPageItem;
        //    if (item != null)
        //    {
        //        Page displaypage = (Page)Activator.CreateInstance(item.TargetType);
        //        await DisplayAlert("", displaypage.ToString(), "OK");

        //    }

        //}
    }
}

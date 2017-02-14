using GantnerMe.Class;
using GantnerMe.Helper;
using GantnerMe.Interface;
using GantnerMe.ViewModel;
using ModernHttpClient;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GantnerMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfilePage : ContentPage
    {
        public UserprofileDB Db = new UserprofileDB();
        public tblUserprofile _tblUserprofile;
        public int _tapCount = 0;
        public UserProfilePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //if (Device.OS == TargetPlatform.Android)
            //{
            //    DependencyService.Get<Isethasnavigationbar>().Show();
            //}
            Title = GlobalUserDetail.CompanyName.ToString();
            bool Status = CrossConnectivity.Current.IsConnected;
            if (Status == false)
            {
                var Getuserprofile = Db.GetUserProfile().ToList();
                if (Getuserprofile.Count() > 0)
                {
                    if (string.IsNullOrWhiteSpace(Getuserprofile[0].userImage))
                    {
                        imguserprofile.Source = "men_ic.png";
                    }
                    else
                    {
                        Byte[] ImageFotoBase64 = System.Convert.FromBase64String(Getuserprofile[0].userImage);
                        imguserprofile.Source = ImageSource.FromStream(() => new MemoryStream(ImageFotoBase64));
                    }
                    lblfullname.Text = Getuserprofile[0].fullName;
                    if (string.IsNullOrWhiteSpace(Getuserprofile[0].email))
                    {
                        lblEmail.Text = "test@email.com";
                    }
                    else
                    {
                        lblEmail.Text = Getuserprofile[0].email;
                    }
                }
                else
                {
                    imguserprofile.Source = "men_ic.png";
                }

            }
            else
            {
                GetProfileDetail();
            }

            ToolbarItems.Add(new ToolbarItem("Home", "home_ic.png", () =>
            {
                SetHomePage();

            }));
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


        public async void GetProfileDetail()
        {
            var loadingPage = new LoadingPopupPage();
            try
            {
                await Navigation.PushPopupAsync(loadingPage);
                using (var client = new HttpClient(new NativeMessageHandler()))
                {

                    // var RestUrl = string.Format(GlobalUserDetail.ServerurlLink+ "/gatac_api/mobile" + "/" + GlobalUserDetail.GlobalGUID + "/Profile");
                    var RestUrl = string.Format(GlobalUserDetail.ServerurlLink + GlobalUserDetail.GlobalGUID + "/Profile");
                    client.BaseAddress = new Uri(RestUrl);
                    HttpResponseMessage response = await client.GetAsync(RestUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var GetobjProfile = JsonConvert.DeserializeObject<UserProfileDetail>(content);
                        Db.DeleteUserProfile();
                        if (GetobjProfile != null)
                        {
                            string Logobase64string = GetobjProfile.picture.src;
                            string image = string.Empty;
                            string Email = string.Empty;
                            string FullName = string.Empty;
                            if (Logobase64string.Contains(","))
                            {
                                image = Logobase64string.Split(',')[1];
                            }
                            _tblUserprofile = new tblUserprofile();
                            _tblUserprofile.fullName = GetobjProfile.fullName;
                            _tblUserprofile.email = GetobjProfile.email;
                            _tblUserprofile.userImage = image;
                            Db.AddUsersProfile(_tblUserprofile);
                            if (string.IsNullOrWhiteSpace(image))
                            {
                                imguserprofile.Source = "men_ic.png";
                            }
                            else
                            {
                                Byte[] ImageFotoBase64 = System.Convert.FromBase64String(image);
                                imguserprofile.Source = ImageSource.FromStream(() => new MemoryStream(ImageFotoBase64));
                            }
                            lblfullname.Text = GetobjProfile.fullName;
                            if (string.IsNullOrWhiteSpace(GetobjProfile.email))
                            {
                                lblEmail.Text = "test@email.com";
                            }
                            else
                            {
                                lblEmail.Text = GetobjProfile.email;
                            }
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await Navigation.RemovePopupPageAsync(loadingPage);
                            });

                        }
                    }
                    else
                    {

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
        public void SetHomePage()
        {
            App.Current.MainPage = new MasterMainPage { Detail = new NavigationPage(new MainPage()) { Title = "Center", BarBackgroundColor = Color.FromHex("#2F6686"), BarTextColor = Color.FromHex("#FFFFFF") } };

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
    }
}

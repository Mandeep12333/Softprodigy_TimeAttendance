using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GantnerMe
{
    public partial class LoadingPopupPage : PopupPage
    {
        public LoadingPopupPage()
        {
            InitializeComponent();
            pageloadindicator.Color = Device.OnPlatform(Color.Black, Color.FromHex("#A6ACAF"), Color.Default);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}

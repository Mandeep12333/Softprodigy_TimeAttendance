using GantnerMe.iOS.CustomRenders;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRenderer))]
namespace GantnerMe.iOS.CustomRenders
{
    public class TabbedPageRenderer : TabbedRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            TabBar.TintColor = UIKit.UIColor.White;
            TabBar.BarTintColor = UIKit.UIColor.FromRGB(59, 115, 150);
            TabBar.BackgroundColor = UIKit.UIColor.FromRGB(56, 119, 150);
        }
    }
}

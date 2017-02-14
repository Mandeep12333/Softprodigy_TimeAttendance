using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using GantnerMe.Droid.CustomRender;

[assembly: ExportRenderer(typeof(WebView), typeof(TransparentWebViewRenderer))]
namespace GantnerMe.Droid.CustomRender
{
    public class TransparentWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            this.Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}
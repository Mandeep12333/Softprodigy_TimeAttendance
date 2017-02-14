using GantnerMe.iOS.CustomRenders;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WebView), typeof(TransparentWebViewRenderer))]
namespace GantnerMe.iOS.CustomRenders
{
    public class TransparentWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            this.Opaque = false;
            this.BackgroundColor = Color.Transparent.ToUIColor();
        }
    }
}

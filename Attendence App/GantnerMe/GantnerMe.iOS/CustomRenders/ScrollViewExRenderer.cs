using GantnerMe.iOS.CustomRenders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ScrollView), typeof(ScrollViewExRenderer))]
namespace GantnerMe.iOS.CustomRenders
{
    public class ScrollViewExRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;

            e.NewElement.PropertyChanged += OnElementPropertyChanged;

        }
        protected void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ShowsHorizontalScrollIndicator = false;
            ShowsVerticalScrollIndicator = false;
        }
    }
}

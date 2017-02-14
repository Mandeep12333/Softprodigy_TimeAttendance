using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Xamarin.Forms;
using GantnerMe.Droid.CustomRender;

[assembly: ExportRenderer(typeof(ScrollView), typeof(ScrollViewExRenderer))]
namespace GantnerMe.Droid.CustomRender
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
            this.HorizontalScrollBarEnabled = false;
            this.VerticalScrollBarEnabled = false;
        }
    }
}
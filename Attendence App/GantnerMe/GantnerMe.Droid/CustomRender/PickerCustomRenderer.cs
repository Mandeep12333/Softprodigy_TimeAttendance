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

[assembly: ExportRenderer(typeof(Picker), typeof(PickerCustomRenderer))]
namespace GantnerMe.Droid.CustomRender
{
    public class PickerCustomRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Gravity = GravityFlags.Left;
               // this.Control.SetBackgroundColor(global::Android.Graphics.Color.Pink);
            }
        }
    }
}
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
using Xamarin.Forms;
using GantnerMe.Droid.CustomRender;

[assembly: ExportRenderer(typeof(ListView), typeof(ListViewScrollBarRender))]
namespace GantnerMe.Droid.CustomRender
{
    public class ListViewScrollBarRender : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Android.Widget.ListView ListView = (Android.Widget.ListView)Control;
                ListView.VerticalScrollBarEnabled = false;
            }
        }
    }
}
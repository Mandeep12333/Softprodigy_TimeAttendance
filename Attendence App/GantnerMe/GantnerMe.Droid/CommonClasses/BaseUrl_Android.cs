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
using GantnerMe.Interface;
using Xamarin.Forms;
using GantnerMe.Droid.CommonClasses;

[assembly: Dependency(typeof(BaseUrl_Android))]
namespace GantnerMe.Droid.CommonClasses
{
    public class BaseUrl_Android : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}
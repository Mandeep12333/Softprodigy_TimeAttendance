using Foundation;
using GantnerMe.Interface;
using GantnerMe.iOS.CommonClasses;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl_iOS))]
namespace GantnerMe.iOS.CommonClasses
{
    public class BaseUrl_iOS : IBaseUrl
    {
        public string Get()
        {
            return NSBundle.MainBundle.BundlePath;
        }
    }
}

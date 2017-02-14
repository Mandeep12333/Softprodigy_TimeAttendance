using GantnerMe.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;
using System.Diagnostics;

namespace GantnerMe
{
    public class L10n
    {
        const string ResourceId = "GantnerMe.Resx.AppResources";
        public static void SetLocale(CultureInfo ci)
        {
            DependencyService.Get<ILocale>().SetLocale(ci);
        }

        [Obsolete]
        public static string Locale(string langCode)
        {
            return DependencyService.Get<ILocale>().GetCurrentCultureInfo(langCode).ToString();
        }

        public static string Localize(string key, string comment,string langCode)
        {
            //var netLanguage = Locale ();

            // Platform-specific
            ResourceManager temp = new ResourceManager(ResourceId, typeof(L10n).GetTypeInfo().Assembly);
            Debug.WriteLine("Localize " + key);
            string result = temp.GetString(key, DependencyService.Get<ILocale>().GetCurrentCultureInfo(langCode));

            if (result == null)
            {
                result = key; // HACK: return the key, which GETS displayed to the user
            }
            return result;
        }

    }
}

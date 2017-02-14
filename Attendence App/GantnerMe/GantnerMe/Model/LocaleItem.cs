using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.Model
{
    public class LocaleItem
    {
        public string LanguageCode { get; set; }
        public string CountryCode { get; set; }
        public string LanguageName { get; set; }
        public string Country { get; set; }

        protected LocaleItem() { }
    }
}

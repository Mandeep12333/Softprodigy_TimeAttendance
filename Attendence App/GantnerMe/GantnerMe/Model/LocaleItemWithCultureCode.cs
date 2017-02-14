using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.Model
{
    public sealed class LocaleItemWithCultureCode : LocaleItem
    {
        public string CultureCode { get; protected internal set; }
    }
}

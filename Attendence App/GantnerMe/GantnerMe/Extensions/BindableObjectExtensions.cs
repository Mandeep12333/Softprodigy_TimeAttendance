using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GantnerMe.Extensions
{
    public static class BindableObjectExtensions
    {
        public static T Get_Value<T>(this BindableObject bindableObject, BindableProperty property)
        {
            return (T)bindableObject.GetValue(property);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.ViewModel
{
    public class GlobalUserDetail
    {
        public static string UserID { get; set; }
        public static string Username { get; set; }
        public static string Email { get; set; }
        public static string DeviceToken { get; set; }
        public static string UserImage { get; set; }
        public static string RatePresentation { get; set; }
        public static string EventFeedbackMessage { get; set; }
        public static string LoginSuccessMessage { get; set; }
        public static string RegisterSuccess { get; set; }
        public static Guid GlobalGUID { get; set; }
        public static string CheckInOut { get; set; }
        public static string CheckInCheckOutType { get; set; }
        public static string CompanyName { get; set; }
        public static string EditLanguage { get; set; }
        public static string UserProfile { get; set; }
        public static string Info { get; set; }
        public static string Settings { get; set; }
        public static int CodeIn { get; set; }
        public static int CodeOut { get; set; }
        public static string ServerurlLink { get; set; }
    }

    public class GlobalLanguageCulture
    {
        public static string LanguageCode { get; set; }
        public static string SelectedLang { get; set; }
    }
}

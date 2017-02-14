using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.ViewModel
{
    public class Logo
    {
        public string src { get; set; }
        public string type { get; set; }
    }

    public class ClsOrganizationProfile
    {
        public string name { get; set; }
        public Logo logo { get; set; }
    }

    public class LoginModelGanter
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class GetAllReasons
    {
        public int codeIn { get; set; }
        public int codeOut { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }
    public class ReasonLangCode
    {
        public string en { get; set; }
        public string ar { get; set; }
    }


    public class Picture
    {
        public string src { get; set; }
        public string type { get; set; }
    }

    public class UserProfileDetail
    {
        public string email { get; set; }
        public string fullName { get; set; }
        public Picture picture { get; set; }
    }

    public class AsignedLocation
    {
        public string description { get; set; }
        public string mapLocation { get; set; }
        public string name { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public double radius { get; set; }
    }
    public class ShiftRecord
    {
        public string timeIn { get; set; }
        public string timeOut { get; set; }
        public string totalHours { get; set; }
    }

    public class MyAttendence
    {
        public string date { get; set; }
        public bool isAbsent { get; set; }
        public bool isHoliday { get; set; }
        public bool isLeave { get; set; }
        public bool isWeekend { get; set; }
        public List<ShiftRecord> shiftRecords { get; set; }
    }

    public class UploadBooking
    {
        public DateTime DateTime { get; set; }
        public int Direction { get; set; }
        public string Location { get; set; }
        public int ReasonCode { get; set; }
    }

    public class ClsReason
    {
        public string CodeIn { get; set; }
        public string CodeOut { get; set; }
        public string ReasonNameIn { get; set; }
        public string ReasonNameOut { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.Class
{

    public class Common
    {
        public TimerSettingDB _Timerdb;
        public tblTimerSettings _tblTimer;

        public Common()
        {
            _Timerdb = new TimerSettingDB();
            _tblTimer = new tblTimerSettings();
        }

        public bool checktimercall(string type)
        {
            var timersetting = _Timerdb.GetTimerSetting();

            if (timersetting == null)
                return false;

            switch (type.ToLower())
            {
                case "organizationprofile":
                    return comparetime(timersetting.OrganizationProfileTime, DateTime.Now);

                case "reasons":
                    return comparetime(timersetting.ReasonsTime, DateTime.Now);

                case "assignedlocations":
                    return comparetime(timersetting.UserAssignedLocationsTime, DateTime.Now);

                default:
                    return false;
            }
        }

        public bool comparetime(DateTime? startdate, DateTime? currentdate)
        {
            // DateTime matchdatetime = startdate.Value.AddHours(5.30);
            if (currentdate >= startdate)
                return true;
            else
                return false;
        }

        public bool updatetimer(string type)
        {

            var timersetting = _Timerdb.GetTimerSetting();
            DateTime? datetoupdate;
            int updatetime = 0;
            if (timersetting == null)
                return false;

            switch (type.ToLower())
            {
                case "organizationprofile":
                    try
                    {
                        datetoupdate = timersetting.OrganizationProfileTime;
                        updatetime = timersetting.OrganizationProfilekey;
                        timersetting.OrganizationProfileTime = DateTime.Now.AddHours(updatetime);
                        _Timerdb.UpdateTimerSetting(timersetting);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                case "reasons":
                    try
                    {
                        datetoupdate = timersetting.ReasonsTime;
                        updatetime = timersetting.ReasonsKey;
                        timersetting.ReasonsTime = DateTime.Now.AddHours(updatetime);
                        _Timerdb.UpdateTimerSetting(timersetting);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                case "assignedlocations":
                    try
                    {
                        // datetoupdate = timersetting.UserAssignedLocationsTime;
                        datetoupdate = timersetting.UserAssignedLocationsTime;
                        updatetime = timersetting.UserAssignedLocationsKey;
                        timersetting.UserAssignedLocationsTime = DateTime.Now.AddHours(updatetime);
                        _Timerdb.UpdateTimerSetting(timersetting);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                default:
                    return false;
            }

        }

        public void addtimervalue()
        {
            tblTimerSettings timer = new tblTimerSettings();
            timer.OrganizationProfilekey = 24;
            timer.ReasonsKey = 24;
            timer.UserAssignedLocationsKey = 1;
            timer.OrganizationProfileTime = System.DateTime.Now.ToLocalTime().AddHours(timer.OrganizationProfilekey);
            timer.ReasonsTime = System.DateTime.Now.ToLocalTime().AddHours(timer.ReasonsKey);
            timer.UserAssignedLocationsTime = System.DateTime.Now.ToLocalTime().AddHours(timer.UserAssignedLocationsKey);
            _Timerdb.AddTimerSetting(timer);
        }

    }
}

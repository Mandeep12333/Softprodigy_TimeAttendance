using GantnerMe.SqlLite;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GantnerMe.Class
{
    public class TimerSettingDB
    {
        private SQLiteConnection _sqlconnection;

        public TimerSettingDB()
        {
            //Getting conection and Creating table  
           
            _sqlconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqlconnection.CreateTable<tblTimerSettings>();
            //_sqlconnection.StoreDateTimeAsTicks = false;
        }

        #region timer

        //Get timer setting values
        public tblTimerSettings GetTimerSetting()
        {
            return _sqlconnection.Table<tblTimerSettings>().FirstOrDefault();
        }

        //Add new  timer setting values to DB  
        public void AddTimerSetting(tblTimerSettings timersetting)
        {
            _sqlconnection.Insert(timersetting);
        }

        public void Deletetimersetting()
        {
            _sqlconnection.DeleteAll<tblTimerSettings>();
        }

        //Update timer setting values to DB  
        public void UpdateTimerSetting(tblTimerSettings timersetting)
        {
             // Deletetimersetting();
           // _sqlconnection.Insert(timersetting);
            _sqlconnection.Update(timersetting);
        }

        #endregion
    }
}

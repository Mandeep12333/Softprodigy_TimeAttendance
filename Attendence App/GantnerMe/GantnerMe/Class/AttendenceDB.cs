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
    public class AttendenceDB
    {
        private SQLiteConnection _sqlconnection;
        public AttendenceDB()
        {
            //Getting conection and Creating table  
            _sqlconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqlconnection.CreateTable<tblAttendence>();
        }
        //Get all//
        public IEnumerable<tblAttendence> GetAttendence()
        {
            return (from t in _sqlconnection.Table<tblAttendence>() select t).ToList();
        }

        //Delete specific//
        public void DeleteAttendence()
        {
            _sqlconnection.DeleteAll<tblAttendence>();
        }

        //Add new//
        public void AddAttendence(tblAttendence model)
        {
            _sqlconnection.Insert(model);
        }

    }
}

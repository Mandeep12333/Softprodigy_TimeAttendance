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
    public class UploadBookingDB
    {
        private SQLiteConnection _sqlconnection;

        public UploadBookingDB()
        {
            //Getting conection and Creating table  
            _sqlconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqlconnection.CreateTable<tblUploadBookings>();


        }

        //Get all students  
        public IEnumerable<tblUploadBookings> GetUploadBookings()
        {
            return (from t in _sqlconnection.Table<tblUploadBookings>() select t).ToList();
        }

        //Delete specific student  
        public void DeleteUploadBookings()
        {
            _sqlconnection.DeleteAll<tblUploadBookings>();
        }

        //Add new student to DB  
        public void AddUploadBookings(tblUploadBookings student)
        {
            _sqlconnection.Insert(student);
        }
    }
}

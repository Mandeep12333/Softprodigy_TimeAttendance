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
   public class ReasonDB
    {
        private SQLiteConnection _sqlconnection;
        public ReasonDB()
        {
            //Getting conection and Creating table  
            _sqlconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqlconnection.CreateTable<tblReason>();
        }

        //Get all//
        public IEnumerable<tblReason> GetReasons()
        {
            return (from t in _sqlconnection.Table<tblReason>() select t).ToList();
        }

        //Delete specific//
        public void DeleteReasons()
        {
            _sqlconnection.DeleteAll<tblReason>();
        }

        //Add new student//
        public void AddReasons(tblReason model)
        {
            _sqlconnection.Insert(model);
        }

    }
}

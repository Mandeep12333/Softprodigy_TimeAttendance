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
    public class CheckInCheckOutDB
    {
        private SQLiteConnection _sqlconnection;

        public CheckInCheckOutDB()
        {
            //Getting conection and Creating table  
            _sqlconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqlconnection.CreateTable<tblCheckInCheckOut>();
        }

        //Get all//
        public IEnumerable<tblCheckInCheckOut> GetCheckInOutLocation()
        {
            return (from t in _sqlconnection.Table<tblCheckInCheckOut>() select t).ToList();
        }

        //Delete specific//
        public void DeleteCheckInOutLocation()
        {
            _sqlconnection.DeleteAll<tblCheckInCheckOut>();
        }

        //Add new// 
        public void AddCheckInOutLocation(tblCheckInCheckOut model)
        {
            _sqlconnection.Insert(model);
        }
    }
}

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
    public class AsignedLocationDB
    {
        private SQLiteConnection _sqlconnection;

        public AsignedLocationDB()
        {
            //Getting conection and Creating table  
            _sqlconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqlconnection.CreateTable<tblAsignedLocation>();
        }

        //Get all//
        public IEnumerable<tblAsignedLocation> GetAsignedLocation()
        {
            return (from t in _sqlconnection.Table<tblAsignedLocation>() select t).ToList();
        }

        //Delete specific//
        public void DeleteAsignedLocation()
        {
            _sqlconnection.DeleteAll<tblAsignedLocation>();
        }

        //Add new//
        public void AddAsignedLocation(tblAsignedLocation Model)
        {
            _sqlconnection.Insert(Model);
        }


    }
}

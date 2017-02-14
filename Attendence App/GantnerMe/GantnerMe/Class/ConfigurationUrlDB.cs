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
    public class ConfigurationUrlDB
    {
        private SQLiteConnection _sqlconnection;

        public ConfigurationUrlDB()
        {
            //Getting conection and Creating table  
            _sqlconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqlconnection.CreateTable<tblConfigurationUrl>();
        }

        //Get all//
        public IEnumerable<tblConfigurationUrl> GetUrl()
        {
            return (from t in _sqlconnection.Table<tblConfigurationUrl>() select t).ToList();
        }

        //Delete specific//
        public void DeleteUrl()
        {
            _sqlconnection.DeleteAll<tblConfigurationUrl>();
        }

        //Add new//
        public void AddUrl(tblConfigurationUrl Model)
        {
            _sqlconnection.Insert(Model);
        }



    }
}

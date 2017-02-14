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
    public class OrganizationProfileDB
    {
        private SQLiteConnection _sqlconnection;

        public OrganizationProfileDB()
        {
            //Getting conection and Creating table  
            _sqlconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqlconnection.CreateTable<OrganizationProfile>();
        }
        //Get all//
        public IEnumerable<OrganizationProfile> GetOrganizationProfile()
        {
            return (from t in _sqlconnection.Table<OrganizationProfile>() select t).ToList();
        }

        public OrganizationProfile GetOrganizationProfile(int id)
        {
            return _sqlconnection.Table<OrganizationProfile>().FirstOrDefault(t => t.Id == id);
        }

        //Delete specific//
        public void DeleteOrganizationProfile()
        {
            _sqlconnection.DeleteAll<OrganizationProfile>();
        }

        //Add new student to DB  
        public void AddOrganizationProfile(OrganizationProfile model)
        {
            _sqlconnection.Insert(model);
        }

    }
}

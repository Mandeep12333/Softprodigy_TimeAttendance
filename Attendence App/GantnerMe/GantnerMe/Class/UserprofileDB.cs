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
    public class UserprofileDB
    {
        private SQLiteConnection _sqlconnection;

        public UserprofileDB()
        {
            //Getting conection and Creating table  
            _sqlconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqlconnection.CreateTable<tblUserprofile>();
        }

        //Get all//
        public IEnumerable<tblUserprofile> GetUserProfile()
        {
            return (from t in _sqlconnection.Table<tblUserprofile>() select t).ToList();
        }

        //Delete specific//
        public void DeleteUserProfile()
        {
            _sqlconnection.DeleteAll<tblUserprofile>();
        }

        //Add new//
        public void AddUsersProfile(tblUserprofile model)
        {
            _sqlconnection.Insert(model);
        }
    }
}

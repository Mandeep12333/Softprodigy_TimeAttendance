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
    public class UserDb
    {
        private SQLiteConnection _sqlconnection;

        public UserDb()
        {
            //Getting conection and Creating table  
            _sqlconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqlconnection.CreateTable<tblUser>();
        }

        //Get all//
        public IEnumerable<tblUser> GetUser()
        {
            return (from t in _sqlconnection.Table<tblUser>() select t).ToList();
        }

        //Delete specific//
        public void DeleteUser()
        {
            _sqlconnection.DeleteAll<tblUser>();
        }

        //Add new//
        public void AddUsers(tblUser model)
        {
            _sqlconnection.Insert(model);
        }
        // Check user is valid//
        public tblUser CheckUserName(string username,string Password)
        {
            return _sqlconnection.Table<tblUser>().Where(x => x.Username == username && x.Password == Password).FirstOrDefault();
        }
    }
}

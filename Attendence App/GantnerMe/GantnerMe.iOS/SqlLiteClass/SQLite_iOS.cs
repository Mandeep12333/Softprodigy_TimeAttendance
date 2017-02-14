using GantnerMe.SqlLite;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net;
using System.IO;
using Xamarin.Forms;
using GantnerMe.iOS.SqlLiteClass;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace GantnerMe.iOS.SqlLiteClass
{
    public class SQLite_iOS : ISQLiteConnection
    {
        public SQLiteConnection GetConnection()
        {
            var fileName = "GantnerMe.db";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);
            var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var connection = new SQLite.Net.SQLiteConnection(platform, path,false);
            return connection;
        }
    }
}

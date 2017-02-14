using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GantnerMe.SqlLite;
using SQLite.Net;
using System.IO;
using Xamarin.Forms;
using GantnerMe.Droid.SqlLiteClass;

[assembly: Dependency(typeof(SQLite_Android))]
namespace GantnerMe.Droid.SqlLiteClass
{
    public class SQLite_Android : ISQLiteConnection
    {
        public SQLiteConnection GetConnection()
        {
            var filename = "GantnerMe.db";
            var documentspath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentspath, filename);
            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var connection = new SQLite.Net.SQLiteConnection(platform, path,false);
            return connection;
        }
    }
}
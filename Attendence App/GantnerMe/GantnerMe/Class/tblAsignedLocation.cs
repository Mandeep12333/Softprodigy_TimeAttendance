using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.Class
{
    public class tblAsignedLocation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string description { get; set; }
        public string mapLocation { get; set; }
        public string name { get; set; }
    }
}

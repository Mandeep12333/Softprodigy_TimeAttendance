using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.Class
{
    public class tblUploadBookings
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Direction { get; set; }
        public string Location { get; set; }
        public int ReasonCode { get; set; }
        public int ReasonCodeIn { get; set; }
    }
}

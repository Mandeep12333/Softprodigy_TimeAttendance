using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.Class
{
    public class tblCheckInCheckOut
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string Address { get; set; }
    }
}

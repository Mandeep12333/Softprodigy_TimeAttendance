using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.Class
{
    public class tblConfigurationUrl
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Url { get; set; }
    }
}

using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.Class
{
    public class tblReason
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int codeIn { get; set; }
        public int codeOut { get; set; }
        public string Reasonid { get; set; }
        public string name { get; set; }
    }
}

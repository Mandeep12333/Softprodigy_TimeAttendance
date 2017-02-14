using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.Class
{
    
    public class tblUserprofile
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }
        public string userImage { get; set; }

    }
}

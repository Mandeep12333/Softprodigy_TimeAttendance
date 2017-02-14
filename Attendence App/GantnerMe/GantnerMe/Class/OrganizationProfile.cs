using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.Class
{
    public class OrganizationProfile
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string OrganizationLogo { get; set; }
        public string Companyname { get; set; }
    }
}

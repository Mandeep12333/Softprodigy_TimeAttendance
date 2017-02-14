using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GantnerMe.Class
{
   public class tblTimerSettings
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int OrganizationProfilekey { get; set; }
        public int ReasonsKey { get; set; }
        public int UserAssignedLocationsKey { get; set; }
        public DateTime? OrganizationProfileTime { get; set; }
        public DateTime? ReasonsTime { get; set; }
        public DateTime? UserAssignedLocationsTime { get; set; }
    }
}

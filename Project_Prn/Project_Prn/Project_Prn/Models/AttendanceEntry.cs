using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Prn.Models
{
    public class AttendanceEntry
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; } = "Present";
        public string Note { get; set; } = "";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Prn.Models
{
    public class StudentInCourse
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Class { get; set; }
        public string School { get; set; }
        public string Status { get; set; } // Pending, Approved, Rejected
        public double AttendanceRate { get; set; }
    }
}

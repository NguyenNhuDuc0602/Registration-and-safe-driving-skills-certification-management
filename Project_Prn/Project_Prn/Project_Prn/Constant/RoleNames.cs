using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Prn.Constant
{
    public static class RoleNames
    {
        public const string Admin = "Admin";
        public const string Teacher = "Teacher";
        public const string Student = "Student";
        public const string TrafficPolice = "TrafficPolice";
        
        public static readonly string[] All = { Admin, Teacher, Student, TrafficPolice };
    }
}

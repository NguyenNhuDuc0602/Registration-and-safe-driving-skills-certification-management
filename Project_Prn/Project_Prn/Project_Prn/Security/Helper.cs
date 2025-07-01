using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Prn.Security
{
    public static class Helper
    {
        public static bool IsInRole(User user, params string[] roles) =>
            roles.Any(role => user.Role.Equals(role, StringComparison.OrdinalIgnoreCase));
    }
}

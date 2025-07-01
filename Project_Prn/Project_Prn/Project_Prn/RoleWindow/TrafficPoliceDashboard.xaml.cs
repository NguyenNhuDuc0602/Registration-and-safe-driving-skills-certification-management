using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project_Prn.RoleWindow
{
    /// <summary>
    /// Interaction logic for TrafficPoliceDashboard.xaml
    /// </summary>
    public partial class TrafficPoliceDashboard : Window
    {
        private readonly User currentUser;
        public TrafficPoliceDashboard(User user)
        {
            InitializeComponent();
            currentUser = new User(); // Initialize with a default user or pass a user object
        }
    }
}

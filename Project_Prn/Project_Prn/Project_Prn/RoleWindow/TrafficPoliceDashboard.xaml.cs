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
            currentUser =  user; // Initialize with a default user or pass a user object
            Window_Loaded(this, null); // Load data when the window is initialized
            DataContext = currentUser; // Bind the current user to the DataContext for data binding
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Prngroup4Context context = new Prngroup4Context();
            // so ki thi  
            PendingExamsText.Text = context.Exams
                .Where(e => e.Course.TeacherId == currentUser.UserId)
                .Select(r => r.ExamId)
                .Distinct()
                .Count().ToString();
            // so chung chi cung cap
            IssuedCertificatesText.Text = context.Certificates
                .Where(c => c.UserId == currentUser.UserId)
                .Count().ToString();

     
        }

    }
}

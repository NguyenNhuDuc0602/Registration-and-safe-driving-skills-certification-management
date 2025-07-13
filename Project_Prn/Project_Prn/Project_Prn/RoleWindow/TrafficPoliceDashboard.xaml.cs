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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn) return;
            string tag = btn.Tag as string ?? string.Empty;
            void Modal(Window child)
            {
                child.Owner = this;
                child.ShowDialog();
                Window_Loaded(this, null); // Reload data after closing the modal
                DataContext = currentUser; // Rebind the current user to the DataContext for data binding
            }
            switch (tag)
            {
                case "MonitorExams":
                    Modal(new ExamWindow.ConfirmExamWindow());
                    break;

                case "AssignTeacher":
                    Modal(new PoliceWindow.AssignTeacherWindow());
                    break;
                case "AssignSupervisor":
                    Modal(new PoliceWindow.AssignSupervisorWindow());
                    break;
                case "ManageCertificates":
                    Modal(new CertificateWindow.CertificateManagement());
                    break;
                case "Compliance":
                    Modal(new ResultWindow.ResultPolice());
                    break;
                case "Logout":
                    var loginWindow = new MainWindow();
                    loginWindow.Show();
                    this.Close();
                    break;
                default:
                    MessageBox.Show("Chức năng chưa được triển khai.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }

        }

    }
}

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
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        private readonly User currentUser;
        public AdminDashboard(User user)
        {
            InitializeComponent();
            currentUser = user;
              DataContext = currentUser; // Bind the current user to the DataContext for data binding
            Window_Loaded(this, null); // Load data when the window is initialized
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn) return;
            string tag = btn.Tag as string ?? string.Empty;

            void Modal (Window child)
            {
                child.Owner = this;
                child.ShowDialog();
                Window_Loaded(this, null); // Reload data after closing the modal
            }
            switch(tag)
            {
                case "Users":
                    Modal(new UserWindow.UserManagement());
                    break;
                case "Courses":
                    Modal(new CourseWindow.CourseManagement());
                    break;
                case "Certificates":
                    Modal(new CertificateWindow.CertificateManagement());
                    break;
                case "Registration":
                    Modal(new RegistrationWindow.RegistrationManagement());
                    break;
                case "Logout":
                    this.Close();
                    break;
                default:
                    MessageBox.Show("Chức năng chưa được triển khai.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             Prngroup4Context context = new Prngroup4Context();
            // so luong nguoi dung
            UserCountText.Text = context.Users.Count().ToString();
            // so luong khoa hoc
            CourseCountText.Text = context.Courses.Count().ToString();
            // so luong bai thi
            ExamCountText.Text = context.Exams.Count().ToString();
            // so luong chung chi
            CertificateCountText.Text = context.Certificates.Count().ToString();
        }
    }
}

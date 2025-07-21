using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using Project_Prn.StudentWindow;
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
    /// Interaction logic for StudentDashboard.xaml
    /// </summary>
    public partial class StudentDashboard : Window
    {
        private readonly User currentUser;

        public StudentDashboard(User user)
        {
            InitializeComponent();
            currentUser = user; // Initialize with a default user or pass a user object
            Window_Loaded(this, null); // Load data when the window is initialized
            DataContext = currentUser; // Bind the current user to the DataContext for data binding
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Prngroup4Context context = new Prngroup4Context();
            // so luong khoa hoc
            RegisteredCoursesText.Text = context.Registrations
                                                .Where(r => r.UserId == currentUser.UserId && r.Status == "Approved")
                                                .Include(r => r.Course)
                                                .Count().ToString();

            // Mới: Chỉ tính các kỳ thi chưa làm và chưa quá hạn
            UpcomingExamsText.Text = context.Registrations
                .Where(r => r.UserId == currentUser.UserId && r.Status == "Approved")
                .Include(r => r.Course)
                    .ThenInclude(c => c.Exams)
                .SelectMany(r => r.Course.Exams)
                .Where(e =>
                    e.Date >= DateOnly.FromDateTime(DateTime.Today) &&
                    !context.Results.Any(res => res.UserId == currentUser.UserId && res.ExamId == e.ExamId)
                )
                .Count()
                .ToString();



            // chung chi dat duoc
            CertificateCountText.Text = context.Certificates
                                                .Where(c => c.UserId == currentUser.UserId)
                                                .Count().ToString();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender is not Button btn) return;
            string tag = btn.Tag as string ?? string.Empty;
            void Modal(Window child)
            {
                child.Owner = this;
                child.ShowDialog();
                Window_Loaded(this, null); // Reload data after closing the modal
            }
            switch (tag)
            {
                case "Registration":
                    Modal(new StudentWindow.CourseRegistrationWindow(currentUser));
                    break;
                case "Profile":
                    var profileWindow = new StudentProfileWindow(currentUser);
                    bool? result = profileWindow.ShowDialog();

                    if (result == true)
                    {
                        // Cập nhật lại giao diện nếu người dùng thay đổi thông tin
                        DataContext = null;
                        DataContext = currentUser;
                    }
                    break;

                case "Progress":
                    Modal(new StudentWindow.StudentResultWindow(currentUser));
                    break;
                case "Exams":
                    Modal(new StudentWindow.ExamViewWindow(currentUser));
                    break;
                case "Learning":
                    Modal(new StudentWindow.CourseLearningWindow(currentUser));
                    break;
                case "Attendance":
                    Modal(new StudentWindow.AttendanceStudentWindow(currentUser));
                    break;
                case "Certificates":
                    Modal(new StudentWindow.CertificateViewWindow(currentUser));
                    break;

                case "Logout":
                    var login = new MainWindow();
                    login.Show();
                    this.Close();
                    break;

                default:
                    MessageBox.Show("Chức năng chưa được triển khai.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }
    }
    
}

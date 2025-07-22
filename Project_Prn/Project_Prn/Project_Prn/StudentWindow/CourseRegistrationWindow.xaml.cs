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
using Microsoft.EntityFrameworkCore;
using Project_Prn.dal;


namespace Project_Prn.StudentWindow
{
    /// <summary>
    /// Interaction logic for CourseRegistrationWindow.xaml
    /// </summary>
    public partial class CourseRegistrationWindow : Window
    {
        private readonly User currentUser;
        private readonly Prngroup4Context context = new();

        public CourseRegistrationWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadAvailableCourses();
        }

        private void LoadAvailableCourses()
        {
            try
            {
                var notificationDao = new NotificationDAO();

                var rejected = context.Registrations
                    .Include(r => r.Course)
                    .Where(r => r.UserId == currentUser.UserId && r.Status == "Rejected")
                    .ToList();

                string message = "";
                foreach (var reg in rejected)
                {
                    // Check null để tránh crash
                    if (reg.Course != null && !notificationDao.IsNotified(currentUser.UserId, reg.RegistrationId))
                    {
                        var comment = string.IsNullOrEmpty(reg.Comments) ? "No reason provided" : reg.Comments;
                        message += $"- {reg.Course.CourseName}: {comment}\n";

                        // Ghi nhận đã thông báo
                        notificationDao.MarkAsNotified(currentUser.UserId, reg.RegistrationId);
                    }
                }

                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show("Bạn đã bị từ chối các khóa học sau:\n\n" + message, "Thông báo từ chối", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Đã xảy ra lỗi khi kiểm tra thông báo từ chối: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // Phần còn lại: hiển thị danh sách khoá học có thể đăng ký
        var registeredIds = context.Registrations
                .Where(r => r.UserId == currentUser.UserId && r.Status != "Rejected")
                .Select(r => r.CourseId)
                .ToList();

            var availableCourses = context.Courses
                .Include(c => c.Teacher)
                .Where(c => !registeredIds.Contains(c.CourseId) && c.EndDate >= DateTime.Now) // Lọc khóa học chưa hết hạn
                .ToList();

            if(!availableCourses.Any())
            {
                MessageBox.Show("Hiện tại không có khóa học khả dụng để đăng ký.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            courseDataGrid.ItemsSource = availableCourses;
        }



        private void RegisterCourseButton_Click(object sender, RoutedEventArgs e)
        {
            if (courseDataGrid.SelectedItem is Course selectedCourse)
            {
                var registration = new Registration
                {
                    UserId = currentUser.UserId,
                    CourseId = selectedCourse.CourseId,
                    Status = "Pending",
                    Comments = null
                };

                context.Registrations.Add(registration);
                context.SaveChanges();

                MessageBox.Show("Đăng ký thành công. Vui lòng chờ xác nhận.",
                                "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadAvailableCourses(); // Reload lại sau khi đăng ký
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khóa học để đăng ký.",
                                "Chưa chọn khóa học", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
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
            // Danh sách các khóa học mà học sinh chưa đăng ký
            var registeredCourseIds = context.Registrations
                .Where(r => r.UserId == currentUser.UserId)
                .Select(r => r.CourseId)
                .ToList();

            var availableCourses = context.Courses
                .Include(c => c.Teacher)
                .Where(c => !registeredCourseIds.Contains(c.CourseId))
                .ToList();

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
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Project_Prn.Attendances; // ← Đã sửa lại đúng namespace chứa AttendanceTracking
using Project_Prn.AttendanceWindow;
using Project_Prn.Models;

namespace Project_Prn.StudentWindow
{
    /// <summary>
    /// Interaction logic for AttendanceStudentWindow.xaml
    /// </summary>
    public partial class AttendanceStudentWindow : Window
    {
        private readonly User _currentUser;
        public AttendanceStudentWindow(User student)
        {
            InitializeComponent();
            _currentUser = student;
            LoadCourses();
        }

        private void LoadCourses()
        {
            using var context = new Prngroup4Context();

            // Lấy danh sách các khóa học đã đăng ký của học sinh
            var registeredCourses = context.Registrations
                .Where(r => r.UserId == _currentUser.UserId && r.Status == "Approved")
                .Include(r => r.Course)
                .Select(r => new 
                {
                    r.Course.CourseId,
                    r.Course.CourseName,
                    StartDate = r.Course.StartDate,
                    EndDate = r.Course.EndDate
                })
                .ToList();

            // Tính tỷ lệ điểm danh sau khi đã lấy dữ liệu
            var courseViews = registeredCourses.Select(course => new StudentCourseView
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                AttendanceRate = CalculateAttendanceRate(course.CourseId)
            }).ToList();

            dgCourses.ItemsSource = courseViews;
        }



        private double CalculateAttendanceRate(int courseId)
        {
            using var context = new Prngroup4Context();

            // Lấy thông tin khóa học
            var course = context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course == null) return 0;

            // Chuyển DateOnly sang DateTime để tính số ngày
            DateTime startDate = course.StartDate;
            DateTime endDate = course.EndDate;

            // Đếm số buổi học đã điểm danh (Present) và số buổi vắng mặt (Absent)
            int presentCount = context.Attendances
                .Where(a => a.CourseId == courseId && a.UserId == _currentUser.UserId && a.Status == "Present")
                .Count();

            int absentCount = context.Attendances
                .Where(a => a.CourseId == courseId && a.UserId == _currentUser.UserId && a.Status == "Absent")
                .Count();

            // Tổng số buổi điểm danh là Present + Absent
            int totalSessions = presentCount + absentCount;

            // Nếu không có buổi nào được điểm danh (Present + Absent == 0), tỷ lệ là 0
            if (totalSessions == 0)
            {
                return 0;
            }

            // Tính tỷ lệ vắng mặt (vắng mặt so với số buổi đã điểm danh)
            double attendanceRate = (presentCount * 100.0) / totalSessions;

            return Math.Round(attendanceRate, 2); // Trả về tỷ lệ vắng mặt
        }




        private void dgCourses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgCourses.SelectedItem == null) return;

            // Vì ItemsSource là anonymous object nên ta cần dùng dynamic
            dynamic selectedCourse = dgCourses.SelectedItem;
            int courseId = selectedCourse.CourseId;

            // Gọi AttendanceTracking với namespace đúng
            var detailWindow = new AttendanceTracking(courseId, _currentUser.UserId);
            detailWindow.Owner = this;
            detailWindow.ShowDialog();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
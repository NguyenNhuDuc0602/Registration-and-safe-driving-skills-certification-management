using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Project_Prn.Models;
using Project_Prn.dal;

namespace Project_Prn.Attendances
{
    public partial class AttendanceWindow : Window
    {
        private readonly int _courseId;
        private readonly Prngroup4Context _dbc;
        private readonly AttendanceDAO _attendanceDAO;
        private readonly RegistrationDAO _registrationDAO;
        private List<AttendanceEntry> attendanceEntries;

        public AttendanceWindow(int courseId)
        {
            InitializeComponent();
            dgAttendance.Columns
                .OfType<DataGridComboBoxColumn>()
                .FirstOrDefault(c => c.Header.ToString() == "Status")
                .ItemsSource = StatusOptions.Values;

            _courseId = courseId;
            _dbc = new Prngroup4Context();
            _attendanceDAO = new AttendanceDAO(_dbc);
            _registrationDAO = new RegistrationDAO(_dbc);
            LoadStudents();
            SetDatePickerRange();
        }

        private void LoadStudents()
        {
            var students = _registrationDAO.GetStudentInCourses(_courseId);
            attendanceEntries = students.Select(s => new AttendanceEntry
            {
                UserId = s.UserID,
                FullName = s.FullName,
                Status = "Present",
                Note = ""
            }).ToList();

            dgAttendance.ItemsSource = attendanceEntries;
        }

        private void SetDatePickerRange()
        {
            var course = _dbc.Courses.FirstOrDefault(c => c.CourseId == _courseId);
            if (course != null)
            {
                DateTime startDate = course.StartDate.ToDateTime(TimeOnly.MinValue);
                DateTime endDate = course.EndDate.ToDateTime(TimeOnly.MinValue);
                dpSessionDate.DisplayDateStart = startDate;
                dpSessionDate.DisplayDateEnd = endDate;

                DateTime today = DateTime.Today;
                dpSessionDate.SelectedDate = (today >= startDate && today <= endDate) ? today : startDate;
            }
        }

        private void SaveAttendance_Click(object sender, RoutedEventArgs e)
        {
            DateTime sessionDate = dpSessionDate.SelectedDate?.Date ?? DateTime.Today;
            var course = _dbc.Courses.FirstOrDefault(c => c.CourseId == _courseId);
            if (course == null)
            {
                MessageBox.Show("Không tìm thấy thông tin khóa học.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Chuyển DateOnly sang DateTime để so sánh
            DateTime startDate = course.StartDate.ToDateTime(TimeOnly.MinValue);
            DateTime endDate = course.EndDate.ToDateTime(TimeOnly.MinValue);

            if (sessionDate < startDate || sessionDate > endDate)
            {
                MessageBox.Show($"Chỉ được điểm danh từ {course.StartDate:dd/MM/yyyy} đến {course.EndDate:dd/MM/yyyy}.",
                                "Ngày không hợp lệ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var entry in attendanceEntries)
            {
                bool exists = _dbc.Attendances.Any(a =>
                    a.CourseId == _courseId && a.UserId == entry.UserId && a.SessionDate.Date == sessionDate.Date);

                if (exists)
                {
                    MessageBox.Show($"Học viên {entry.FullName} đã được điểm danh vào ngày này.",
                        "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            var attendances = attendanceEntries.Select(e => new Attendance
            {
                CourseId = _courseId,
                UserId = e.UserId,
                SessionDate = sessionDate,
                Status = e.Status,
                Note = e.Note
            }).ToList();

            _attendanceDAO.AddBulkAttendance(attendances);
            MessageBox.Show("Điểm danh thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }
    }
}


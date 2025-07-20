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
using Project_Prn.AttendanceWindow;
using Project_Prn.dal;
using Project_Prn.Models;

namespace Project_Prn.Attendances
{
    /// <summary>
    /// Interaction logic for AttendanceViewWindow.xaml
    /// </summary>
    public partial class AttendanceViewWindow : Window
    {
        private readonly Prngroup4Context _dbc;
        private readonly CourseDAO _courseDAO;
        private readonly AttendanceDAO _attendanceDAO;
        private readonly RegistrationDAO _registrationDAO;
        private readonly int _courseId;

        public AttendanceViewWindow(int courseId)
        {
            InitializeComponent();
            _courseId = courseId;
            _dbc = new Prngroup4Context();
            _attendanceDAO = new AttendanceDAO(_dbc);
            _registrationDAO = new RegistrationDAO(_dbc);
            LoadAttendance();
        }

        private void LoadAttendance()
        {
            var students = _registrationDAO.GetStudentInCourses(_courseId);
            int totalSessions = _attendanceDAO.CountTotalSessions(_courseId);

            var result = students.Select(s =>
            {
                int presentCount = _attendanceDAO.CountPresentSessions(_courseId, s.UserID);
                double rate = totalSessions == 0 ? 0 : (presentCount * 100.0 / totalSessions);
                string status = rate >= 80 ? "Đủ điều kiện" : "Chưa đủ";

                return new
                {
                    s.UserID,
                    s.FullName,
                    TotalSessions = totalSessions,
                    PresentSessions = presentCount,
                    AttendanceRate = Math.Round(rate, 2),
                    Eligibility = status
                };
            }).ToList();

            dgStudentAttendance.ItemsSource = result;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgStudentAttendance_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgStudentAttendance.SelectedItem == null)
                return;

            // Ép kiểu động để lấy UserID
            dynamic selected = dgStudentAttendance.SelectedItem;
            int userId = selected.UserID;

            var trackingWindow = new AttendanceTracking(_courseId, userId);
            trackingWindow.ShowDialog();
        }
    }
}


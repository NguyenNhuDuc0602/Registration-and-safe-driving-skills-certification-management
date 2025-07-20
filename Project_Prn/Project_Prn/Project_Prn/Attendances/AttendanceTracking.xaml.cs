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
using Microsoft.VisualBasic.ApplicationServices;
using Project_Prn.dal;
using Project_Prn.Models;

namespace Project_Prn.AttendanceWindow
{
    /// <summary>
    /// Interaction logic for AttendanceTracking.xaml
    /// </summary>
    public partial class AttendanceTracking : Window
    {
        private readonly int _courseId;
        private readonly int _userId;
        private readonly Prngroup4Context _dbc;
        private readonly AttendanceDAO _attendanceDAO;
        public AttendanceTracking(int courseId, int userId)
        {
            InitializeComponent();
            this._courseId = courseId;
            this._userId = userId;

            _dbc = new Prngroup4Context();
            _attendanceDAO = new AttendanceDAO(_dbc);
            LoadAttendance();
        }

        private void LoadAttendance()
        {
            var user = _dbc.Users.Find(_userId);
            txtHeader.Text = $"Attendance of: {user.FullName}";

            var details = _attendanceDAO.GetAttendanceDetails(_courseId, _userId);
            dgAttendance.ItemsSource = details;
        }
    }
}


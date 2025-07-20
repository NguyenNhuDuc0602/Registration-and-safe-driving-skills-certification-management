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

namespace Project_Prn.CourseWindow
{
    /// <summary>
    /// Interaction logic for CourseStudentList.xaml
    /// </summary>
    public partial class CourseStudentList : Window
    {
        private readonly Prngroup4Context _dbc;
        private readonly RegistrationDAO _registrationDAO;
        private readonly int _courseId;
        public CourseStudentList(int courseId)
        {
            InitializeComponent();
            _dbc = new Prngroup4Context();
            Prngroup4Context context = new Prngroup4Context();
            _registrationDAO = new RegistrationDAO(context);
            this._courseId = courseId;
            LoadStudentList();
        }

        private void LoadStudentList()
        {
            var students = _registrationDAO.GetStudentInCourses(_courseId);
            studentDataGrid.ItemsSource = students;
        }

        private void studentDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (studentDataGrid.SelectedItem is StudentInCourse student)
            {
                var trackingWindow = new AttendanceTracking(_courseId, student.UserID);
                trackingWindow.ShowDialog();
            }
        }


    }
}

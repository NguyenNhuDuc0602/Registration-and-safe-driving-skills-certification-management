using Project_Prn.dal;
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

namespace Project_Prn.CourseWindow
{
    /// <summary>
    /// Interaction logic for CourseManagement.xaml
    /// </summary>
    public partial class CourseManagement : Window
    {
        public CourseManagement()
        {
            InitializeComponent();
            LoadCourse();
        }
        void LoadCourse()
        {
           CourseDAO courseDAO = new CourseDAO();
              var courses = courseDAO.GetAllCourse();
            this.dgCourses.ItemsSource = courses;
        }
    }
}

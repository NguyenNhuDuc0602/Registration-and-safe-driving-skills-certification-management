<<<<<<< HEAD
﻿using Project_Prn.dal;
using Project_Prn.Models;
using System;
=======
﻿using System;
>>>>>>> 1aee48a4477379607c2632efdc1c4d41d78b0c06
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
<<<<<<< HEAD
=======
using Project_Prn.dal;
>>>>>>> 1aee48a4477379607c2632efdc1c4d41d78b0c06

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
<<<<<<< HEAD
           CourseDAO courseDAO = new CourseDAO();
              var courses = courseDAO.GetAllCourse();
            this.dgCourses.ItemsSource = courses;
        }
=======
            CourseDAO courseDAO = new CourseDAO();
            var course = courseDAO.GetAllCourse();
            this.dgCourses.ItemsSource = course;
        }

>>>>>>> 1aee48a4477379607c2632efdc1c4d41d78b0c06
    }
}

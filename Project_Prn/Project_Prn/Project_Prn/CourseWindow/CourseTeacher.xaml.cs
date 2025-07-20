using Project_Prn.Models;
using Project_Prn.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Project_Prn.CourseWindow
{
    public partial class CourseTeacher : Window
    {
        private User currentTeacher;

        public CourseTeacher(User teacher)
        {
            InitializeComponent();
            currentTeacher = teacher;
            LoadMyCourses();
        }

        private void LoadMyCourses()
        {
            CourseDAO courseDAO = new CourseDAO();
            var courses = courseDAO.GetByTeacherIdCourse(currentTeacher.UserId);
            dgMyCourses.ItemsSource = courses;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            CourseDAO courseDAO = new CourseDAO();
            var filteredCourses = courseDAO.GetByTeacherIdCourse(currentTeacher.UserId)
                                           .Where(c => c.CourseName.ToLower().Contains(keyword))
                                           .ToList();

            dgMyCourses.ItemsSource = filteredCourses;

            if (filteredCourses.Count == 0)
            {
                MessageBox.Show("Không tìm thấy khóa học nào phù hợp.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

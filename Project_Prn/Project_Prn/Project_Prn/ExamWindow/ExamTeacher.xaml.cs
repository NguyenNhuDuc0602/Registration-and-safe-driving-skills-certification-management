using Project_Prn.dal;
using Project_Prn.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Project_Prn.ExamWindow
{
    public partial class ExamTeacher : Window
    {
        private User currentTeacher;
        private ExamDAO examDAO = new ExamDAO();
        private CourseDAO courseDAO = new CourseDAO();

        public ExamTeacher(User teacher)
        {
            InitializeComponent();
            currentTeacher = teacher;
            LoadCourseFilter();
            LoadExams();
        }

        private void LoadCourseFilter()
        {
            var courses = courseDAO.GetByTeacherIdCourse(currentTeacher.UserId);
            courses.Add(new Course { CourseId = -1, CourseName = "All Courses" });

            cbxCourseFilter.ItemsSource = courses;
            cbxCourseFilter.DisplayMemberPath = "CourseName";
            cbxCourseFilter.SelectedValuePath = "CourseId";
            cbxCourseFilter.SelectedValue = -1;
        }

        private void LoadExams(int courseId = -1)
        {
            var exams = examDAO.GetAllExam()
                .Where(e => e.Course.TeacherId == currentTeacher.UserId || e.SupervisorId == currentTeacher.UserId)
                .ToList();

            if (courseId > 0)
                exams = exams.Where(e => e.CourseId == courseId).ToList();

            dgExamTeacher.ItemsSource = exams;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (cbxCourseFilter.SelectedValue != null)
            {
                int selectedCourseId = int.Parse(cbxCourseFilter.SelectedValue.ToString());
                LoadExams(selectedCourseId);
            }
        }

        private void dgExamTeacher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Optional: show exam detail in popup
        }
    }
}

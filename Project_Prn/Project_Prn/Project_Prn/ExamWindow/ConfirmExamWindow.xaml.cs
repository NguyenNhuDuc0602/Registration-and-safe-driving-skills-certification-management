using Project_Prn.dal;
using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Project_Prn.ExamWindow
{
    public partial class ConfirmExamWindow : Window
    {
        private ExamDAO examDAO = new ExamDAO();
        private CourseDAO courseDAO = new CourseDAO(); // Giả sử bạn có CourseDAO để lấy danh sách Course

        public ConfirmExamWindow()
        {
            InitializeComponent();
            LoadCourses();
            LoadExams();
        }

        // Load danh sách khóa học vào ComboBox
        private void LoadCourses()
        {
            var courses = courseDAO.GetAllCourse(); // cần viết DAO nếu chưa có
            cbxCourse.ItemsSource = courses;
            cbxCourse.DisplayMemberPath = "CourseName";
            cbxCourse.SelectedValuePath = "CourseId";
        }

        // Load tất cả kỳ thi chưa xác nhận hoặc theo khóa học (nếu được chọn)
        private void LoadExams(int? courseId = null)
        {
            var allExams = examDAO.GetAllExam()
                .Where(e => e.IsConfirmed == false);

            if (courseId != null)
            {
                allExams = allExams.Where(e => e.CourseId == courseId);
            }

            dgExamList.ItemsSource = allExams.ToList();
        }

        // Sự kiện chọn lọc theo khóa học
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            if (cbxCourse.SelectedValue != null)
            {
                int courseId = (int)cbxCourse.SelectedValue;
                LoadExams(courseId);
            }
        }

        // Bắt sự kiện click nút xác nhận trong từng dòng
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                Exam selectedExam = (Exam)((FrameworkElement)btn).DataContext;
                if (selectedExam != null)
                {
                    examDAO.ConfirmExam(selectedExam.ExamId);
                    MessageBox.Show($"Đã xác nhận kỳ thi ID: {selectedExam.ExamId}", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Tải lại danh sách sau khi xác nhận
                    if (cbxCourse.SelectedValue != null)
                    {
                        LoadExams((int)cbxCourse.SelectedValue);
                    }
                    else
                    {
                        LoadExams();
                    }
                }
            }
        }

        private void dgExamList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Không cần xử lý gì ở đây nếu bạn dùng nút xác nhận trong từng dòng
        }
    }
}

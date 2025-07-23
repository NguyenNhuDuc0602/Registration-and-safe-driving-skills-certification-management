using System;
using System.Collections.Generic;
using System.Data;
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
using Project_Prn.dal;
using Project_Prn.Models;
using Project_Prn.UserWindow;

namespace Project_Prn.ExamWindow
{
    /// <summary>
    /// Interaction logic for ExamManager.xaml
    /// </summary>
    public partial class ExamManager : Window
    {
        public string examId { get; set; }
        private ExamDAO ExamDAO = new ExamDAO();
        public ExamManager()
        {
            InitializeComponent();
            loadExam();
        }
        public void loadExam()
        {
            var Exam = ExamDAO.GetAllExam();
            this.dgExam.ItemsSource = Exam;

            CourseDAO courseDAO = new CourseDAO();
            var course = courseDAO.GetAllCourse();
            course.Add(new Course()
            {
                CourseId = -1,
                CourseName ="ALL"
            });


            this.cbxCourse.ItemsSource = course;//combobox
            this.cbxCourse.DisplayMemberPath = "CourseName";
            this.cbxCourse.SelectedValuePath = "CourseId";
            this.cbxCourse.SelectedValue = 2;
        }
        public void loadExam(int courseId)
        {
            var exam = ExamDAO.GetByCourseIdExam(courseId);
            this.dgExam.ItemsSource = exam;
        }

        private void dgExam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedExam = dgExam.SelectedItem as Exam;
            if (selectedExam != null)
            {
                examId = selectedExam.ExamId.ToString();
            }

        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int courseId = Int32.Parse(this.cbxCourse.SelectedValue.ToString());
            if (courseId > 0)
            {
                loadExam(courseId);
            }
            else
            {
                loadExam();
            }
        }
        private void addExam_Click(object sender, RoutedEventArgs e)
        {
            AddExam addExam = new AddExam();
            addExam.ShowDialog();
            loadExam();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgExam.SelectedItem is Exam selectedExam)
            {
                editExam edit = new editExam(selectedExam.ExamId.ToString());
                bool? result = edit.ShowDialog();
                if (result == true)
                {
                   
                    loadExam();
                }

            }
            else
            {
                MessageBox.Show("Vui lòng chọn một kỳ thi để sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }





        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa kỳ thi này không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                ExamDAO examDAO = new ExamDAO();
                int id = int.Parse(examId);
                examDAO.DeleteExam(id);

                MessageBox.Show("Xóa kỳ thi thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                loadExam();
                //this.Close(); 
            }
        }

    }
}

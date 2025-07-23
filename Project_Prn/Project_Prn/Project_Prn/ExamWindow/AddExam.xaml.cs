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
using Project_Prn.dal;
using Project_Prn.Models;

namespace Project_Prn.ExamWindow
{
    /// <summary>
    /// Interaction logic for AddExam.xaml
    /// </summary>
    public partial class AddExam : Window
    {
        private ExamDAO examDAO = new ExamDAO();

        public AddExam()
        {
            InitializeComponent();
            LoadCourse();
        }
        public void LoadCourse()
        {
            CourseDAO courseDAO = new CourseDAO();
            var course = courseDAO.GetAllCourse();
            this.cbCourse.ItemsSource = course;//combobox
            this.cbCourse.DisplayMemberPath = "CourseName";
            this.cbCourse.SelectedValuePath = "CourseId";
            this.cbCourse.SelectedValue = 2;
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbCourse.SelectedValue == null || dpDate.SelectedDate == null || string.IsNullOrWhiteSpace(txtRoom.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var newExam = new Exam
            {
                CourseId = (int)cbCourse.SelectedValue,
                ExamDate = dpDate.SelectedDate.Value, 
                Room = txtRoom.Text.Trim()
            };


            examDAO.AddExam(newExam);

            MessageBox.Show("Thêm kỳ thi thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true; // Để ExamManagerWindow biết cần reload
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}

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
    /// Interaction logic for editExam.xaml
    /// </summary>
    public partial class editExam : Window
    {
        public readonly string _ExamId;
        public editExam(string examId)
        {
            this._ExamId = examId;
            InitializeComponent();

            CourseDAO courseDAO = new CourseDAO();
            ExamDAO examDAO = new ExamDAO();
            Exam exam = examDAO.GetByIdExam(Int32.Parse(this._ExamId));
            if (exam != null)
            {
                this.cbxCourse.ItemsSource = courseDAO.GetAllCourse(); // combobox
                this.cbxCourse.DisplayMemberPath = "CourseName";
                this.cbxCourse.SelectedValuePath = "CourseId";
                this.cbxCourse.SelectedValue = exam.CourseId;             
                this.dpdate.SelectedDate = exam.ExamDate;

                this.txtClass.Text = exam.Room;
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int CourseId = Int32.Parse(this.cbxCourse.SelectedValue.ToString());
            DateTime? selectedDate = dpdate.SelectedDate;

            if (selectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày thi.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string room = this.txtClass.Text;

            ExamDAO examDAO = new ExamDAO();
            var oldExam = examDAO.GetByIdExam(Int32.Parse(this._ExamId));

            if (oldExam == null)
            {
                MessageBox.Show("Không tìm thấy kỳ thi để cập nhật!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //  Gán lại thông tin
            oldExam.CourseId = CourseId;
            oldExam.ExamDate = selectedDate.Value;
            oldExam.Room = room;

            examDAO.UpdateExam(oldExam);

            this.DialogResult = true;
            this.Close();
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();

        }
    }
}

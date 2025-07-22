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
                this.cbxCourse.ItemsSource = courseDAO.GetAllCourse();//combobox
                this.cbxCourse.DisplayMemberPath = "CourseName";
                this.cbxCourse.SelectedValuePath = "CourseId";
                this.cbxCourse.SelectedValue = exam.CourseId;
                this.dpdate.SelectedDate = exam.Date.ToDateTime(TimeOnly.MinValue);
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

            Exam updateExam = new Exam()
            {
                ExamId = Int32.Parse(this._ExamId),
                CourseId = CourseId,
                Date = DateOnly.FromDateTime(selectedDate.Value),
                ExamDate = selectedDate.Value, // ✅ FIX: Gán ExamDate hợp lệ để tránh lỗi SQL
                Room = room,
            };

            ExamDAO examDAO = new ExamDAO();
            examDAO.UpdateExam(updateExam);

            this.DialogResult = true; // báo hiệu thành công
            this.Close();
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();

        }
    }
}

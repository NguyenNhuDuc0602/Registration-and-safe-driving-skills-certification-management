using Project_Prn.dal;
using Project_Prn.Models;
using Project_Prn.UserWindow;
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
using System.Xml.Linq;

namespace Project_Prn.CourseWindow
{
    /// <summary>
    /// Interaction logic for AddCourse.xaml
    /// </summary>
    public partial class AddCourse : Window
    {
        private CourseManagement coursemanager;

        public AddCourse()
        {
            InitializeComponent();
        }

        public AddCourse( CourseManagement courseManagement)
        {
            InitializeComponent();
            coursemanager = courseManagement;
            LoadTeachers(); // Tải danh sách giảng viên khi khởi tạo cửa sổ
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtCourseName.Text.Trim();
                int teacherId = (int)cbTeacher.SelectedValue;

                var start = dpStartDate.SelectedDate;
                var end = dpEndDate.SelectedDate;

                if (start == null || end == null)
                {
                    MessageBox.Show("Please select valid Start Date and End Date!");
                    return;
                }

                if (end < start)
                {
                    MessageBox.Show("End Date cannot be earlier than Start Date!");
                    return;
                }

                var dao = new CourseDAO();
                var newCourse = new Course
                {
                    CourseName = name,
                    TeacherId = teacherId,
                    StartDate = DateOnly.FromDateTime(start.Value),
                    EndDate = DateOnly.FromDateTime(end.Value)
                };

                dao.AddCourse(newCourse);
                MessageBox.Show("Course added successfully!");

                // 🡒 Gọi reload lại danh sách Course trên CourseManagement
                coursemanager.LoadCourse();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void LoadTeachers()
        {
            var dao = new CourseDAO();
            cbTeacher.ItemsSource = dao.GetAllTeachers(); 
            cbTeacher.DisplayMemberPath = "FullName";    
            cbTeacher.SelectedValuePath = "UserId";      
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {         
            this.Close(); // Đóng cửa sổ AddCourse
        }
    }
}




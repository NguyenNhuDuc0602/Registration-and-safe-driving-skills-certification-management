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
            // 1. Lấy dữ liệu từ form
            string name = txtCourseName.Text;
            int teacherId = (int)cbTeacher.SelectedValue;
            string start = dpStartDate.Text;
            string end = dpEndDate.Text;

            // 2. Thêm vào DB
            var dao = new CourseDAO();
            var newCourse = new Course
            {
                CourseName = name,
                TeacherId = teacherId,
                StartDate = DateOnly.FromDateTime(DateTime.Parse(start)),
                EndDate = DateOnly.FromDateTime(DateTime.Parse(end))
            };
            dao.AddCourse(newCourse);
            coursemanager.LoadCourse(); // Cập nhật danh sách khóa học trong CourseManagement
            this.Close(); // Đóng cửa sổ AddCourse



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




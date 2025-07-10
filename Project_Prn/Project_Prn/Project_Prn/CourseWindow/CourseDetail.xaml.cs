using Microsoft.VisualBasic.ApplicationServices;
using Project_Prn.dal;
using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
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

namespace Project_Prn.CourseWindow
{
    /// <summary>
    /// Interaction logic for CourseDetail.xaml
    /// </summary>
    public partial class CourseDetail : Window
    {
        private readonly Course course;
        public CourseDetail(Course course)
        {
            InitializeComponent();
            this.course = course;
            LoadTeachers(); // Tải danh sách giảng viên khi khởi tạo cửa sổ
            LoadCourseDetails(); // Tải thông tin khóa học khi khởi tạo cửa sổ
        }
    
      private void LoadCourseDetails()
        {
            txtCourseName.Text = course.CourseName;
             cbTeacher.SelectedValue = course.TeacherId; 
            dpStartDate.SelectedDate = course.StartDate.ToDateTime(new TimeOnly(0, 0));
            dpEndDate.SelectedDate = course.EndDate.ToDateTime(new TimeOnly(0, 0));
        }
        private void LoadTeachers()
        {
            UserDAO userDao = new UserDAO();
            cbTeacher.ItemsSource = userDao.GetByRoleUser("Teacher"); 
            cbTeacher.DisplayMemberPath = "FullName";
            cbTeacher.SelectedValuePath = "UserId"; // phải khớp với course.TeacherId
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Đóng cửa sổ CourseDetail
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            course.CourseName = txtCourseName.Text;
            course.Teacher = (Models.User)cbTeacher.SelectedItem; // Lấy đối tượng User từ ComboBox
            course.StartDate = DateOnly.FromDateTime(dpStartDate.SelectedDate.Value);
            course.EndDate = DateOnly.FromDateTime(dpEndDate.SelectedDate.Value);

            var CourseDAO = new CourseDAO();
            CourseDAO.UpdateCourse(course); 
            this.DialogResult = true; // Đặt kết quả của cửa sổ là true để thông báo cập nhật thành công
            Close(); // Đóng cửa sổ chi tiết người dùng hiện tại
        }
    }
}

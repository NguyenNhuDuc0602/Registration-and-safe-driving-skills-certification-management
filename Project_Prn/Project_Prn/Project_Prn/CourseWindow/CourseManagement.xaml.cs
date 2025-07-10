using Microsoft.IdentityModel.Tokens;
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

namespace Project_Prn.CourseWindow
{
    /// <summary>
    /// Interaction logic for CourseManagement.xaml
    /// </summary>
    public partial class CourseManagement : Window
    {
        public CourseManagement()
        {
            InitializeComponent();
            LoadCourse();
        }
        public void LoadCourse()
        {
            CourseDAO courseDAO = new CourseDAO();
            var courses = courseDAO.GetAllCourse();
            this.dgCourses.ItemsSource = courses;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            AddCourse addCourseWindow = new AddCourse(this);
            addCourseWindow.ShowDialog();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var selectCourse = this.dgCourses.SelectedItem as Course;
            if (selectCourse == null)
            {
                MessageBox.Show("Vui lòng chọn một khoóa học.");
                return;
            }
            var detailWindow = new CourseDetail(selectCourse);
            bool? updated = detailWindow.ShowDialog(); // tra ve true neu nut update duoc nhan
            if (updated == true)
            {
                LoadCourse(); 
            }
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourse = this.dgCourses.SelectedItem as Course;
            if (selectedCourse == null)
            {
                MessageBox.Show("Vui lòng chọn một khóa học để xóa.");
                return;
            }
            CourseDAO courseDao = new CourseDAO();
            courseDao.DeleteCourse(selectedCourse.CourseId);
            LoadCourse(); 
            MessageBox.Show("Khóa học đã được xóa thành công.");
        }
        public void LoadCoursesByName(string name)
        {
            CourseDAO userDAO = new CourseDAO();
            var courses = userDAO.GetCourseBylName(name);
            this.dgCourses.ItemsSource = courses;
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (!searchText.IsNullOrEmpty())
            {
                LoadCoursesByName(searchText);
                if (dgCourses.AllowDrop == null || dgCourses.Items.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy khoá học nào với tên này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                LoadCourse(); 
            }
        }
    }
}



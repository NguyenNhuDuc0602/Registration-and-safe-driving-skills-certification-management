using Project_Prn.dal;
using Project_Prn.Models;
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
namespace Project_Prn.StudentWindow
{
    public partial class CourseLearningWindow : Window
    {
        private readonly User currentUser;

        public CourseLearningWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadCourses();
        }

        private void LoadCourses()
        {
            Prngroup4Context context = new Prngroup4Context();
            RegistrationDAO dao = new RegistrationDAO(context);
            var courses = dao.GetByUserIdRegistration(currentUser.UserId)
                             .Where(r => r.Status == "Approved")
                             .ToList();
            dgCourses.ItemsSource = courses;
        }

        private void btnLearn_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgCourses.SelectedItem as Registration;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn khóa học để vào học.");
                return;
            }

            MessageBox.Show($"Đang vào học khóa: {selected.Course.CourseName}", "Vào học",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
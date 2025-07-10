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

namespace Project_Prn.RoleWindow
{
    /// <summary>
    /// Interaction logic for TeacherDashboard.xaml
    /// </summary>
    public partial class TeacherDashboard : Window
    {
        private readonly User currentUser;
        public TeacherDashboard(User user)
        {
            InitializeComponent();
            currentUser = user;// Initialize with a default user or pass a user object
            Window_Loaded(this, null); // Load data when the window is initialized
            DataContext = currentUser; // Bind the current user to the DataContext for data binding
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender is not Button btn) return;
            string tag = btn.Tag as string ?? string.Empty;
            void Modal(Window child)
            {
                child.Owner = this;
                child.ShowDialog();
            Window_Loaded(this, null); // Reload data after closing the modal
                DataContext = currentUser; // Rebind the current user to the DataContext for data binding
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Prngroup4Context context = new Prngroup4Context();
            // so luong hoc sinh 
            StudentsCountText.Text = context.Registrations
                .Where(r => r.Course.TeacherId == currentUser.UserId)
                .Select(r => r.UserId)
                .Distinct() 
                .Count().ToString();
            // so luong khoa hoc ma giang vien day
            CoursesCountText.Text = context.Courses
                .Where(c => c.TeacherId == currentUser.UserId)
                .Count().ToString();

            // so luong ki thi
            ExamsCountText.Text = context.Exams
                .Where(e => e.Course.TeacherId == currentUser.UserId)
                .Count().ToString();

        }
    }
}

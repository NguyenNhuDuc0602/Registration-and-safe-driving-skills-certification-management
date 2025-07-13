using Project_Prn.RoleWindow;
using Project_Prn.Constant;
using Project_Prn.dal;
using Project_Prn.Models;
using Project_Prn.UserWindow;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_Prn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            UserDAO userdao = new UserDAO();
            User? user = userdao.ValidateLogin(email, password);
            if (user == null)
            {
                MessageBox.Show("Invalid email or password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show($"Welcome {user.FullName}!", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
          

            // phan quyen
            Window nextWindow = user.Role switch
            {
                RoleNames.Admin => new AdminDashboard(user),
                RoleNames.Teacher => new TeacherDashboard(user),
                RoleNames.TrafficPolice => new TrafficPoliceDashboard(user),
                _     => new StudentDashboard(user)
            };
            nextWindow.Show(); // Show the appropriate dashboard based on the user's role
            this.Hide(); // Hide the login window

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog(); // Hiển thị dưới dạng modal, đợi đóng rồi mới quay lại
        }
    }
}
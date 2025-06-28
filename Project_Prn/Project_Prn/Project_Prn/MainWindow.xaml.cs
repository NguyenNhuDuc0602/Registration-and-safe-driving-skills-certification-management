using Project_Prn.dal;
using Project_Prn.Models;
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
using Project_Prn.UserWindow;

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

        private  void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            UserDAO userdao = new UserDAO();
            User? user = userdao.ValidateLogin(email, password);
            if(user == null)
            {
                MessageBox.Show("Invalid email or password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show($"Welcome {user.FullName}!", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            UserManagement userManagement = new UserManagement();
            userManagement.Show();
            this.Close(); // Close the login window after successful login
            
            

        }
    }
}
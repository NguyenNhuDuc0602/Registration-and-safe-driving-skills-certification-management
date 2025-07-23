using Project_Prn.dal;
using Project_Prn.Models;
using System.Windows;
using System.Windows.Controls;

namespace Project_Prn
{
    public partial class RegisterWindow : Window
    {
        private UserDAO userDAO;

        public RegisterWindow()
        {
            InitializeComponent();
            userDAO = new UserDAO();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password;
            string confirm = txtConfirmPassword.Password;
            string role = ((ComboBoxItem)cbRole.SelectedItem).Content.ToString();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (userDAO.GetByEmailUser(email) != null)
            {
                MessageBox.Show("Email đã được sử dụng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newUser = new User
            {
                FullName = name,
                Email = email,
                Password = password,
                Role = role
            };

            userDAO.AddUser(newUser);

            MessageBox.Show("Đăng ký thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}

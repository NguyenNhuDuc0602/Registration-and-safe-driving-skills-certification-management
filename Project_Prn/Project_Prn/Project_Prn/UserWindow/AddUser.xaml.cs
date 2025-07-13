using Project_Prn.dal;
using Project_Prn.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Project_Prn.UserWindow
{
    public partial class AddUser : Window
    {
        private UserManagement userManagementWindow;
        private StudentManagement studentManagementWindow;

        public AddUser(UserManagement window)
        {
            InitializeComponent();
            userManagementWindow = window;
            LoadRoles();
        }

        public AddUser(StudentManagement window)
        {
            InitializeComponent();
            studentManagementWindow = window;
            LoadRoles();
        }

        private void LoadRoles()
        {
            using (var context = new Prngroup4Context())
            {
                var roles = context.Users
                    .Select(u => u.Role)
                    .Distinct()
                    .ToList();

                cmbRole.ItemsSource = roles;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text;
            string email = txtEmail.Text;
            string classes = txtClass.Text;
            string phone = txtPhone.Text;
            string school = txtSchool.Text;
            string pass = txtPassword.Password;
            string role = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc.");
                return;
            }

            UserDAO userDAO = new UserDAO();

            if (userDAO.GetByEmailUser(email) != null)
            {
                MessageBox.Show("Email đã tồn tại. Vui lòng chọn email khác.");
                return;
            }

            User newUser = new User()
            {
                FullName = fullName,
                Email = email,
                Class = classes,
                Phone = phone,
                School = school,
                Role = role,
                Password = pass
            };

            userDAO.AddUser(newUser);

            userManagementWindow?.LoadUsers();
            studentManagementWindow?.LoadUsers();

            this.Close();
        }

        private void cmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Optional feedback when role is changed
        }
    }
}

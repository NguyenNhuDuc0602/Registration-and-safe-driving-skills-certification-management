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

namespace Project_Prn.UserWindow
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private UserManagement userManagementWindow; // tham chieu den cua so UserManagement
        public AddUser(UserManagement userManagement)
        {
            InitializeComponent();
          this.userManagementWindow = userManagement; // Luu tham chieu cua so UserManagement
            LoadRoles(); 
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string fullName = this.txtFullName.Text;
            string email = this.txtEmail.Text;
            string classes = this.txtClass.Text;
            string phone = this.txtPhone.Text;
            string school = this.txtSchool.Text;
            string pass = this.txtPassword.Password;
            string role = this.cmbRole.SelectedItem.ToString();

            UserDAO userDAO = new UserDAO();

            // Kiểm tra xem email đã tồn tại chưa
            var existingUser = userDAO.GetByEmailUser(email);
            if (existingUser != null)
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
            userManagementWindow.LoadUsers(); // Cập nhật danh sách người dùng trong UserManagement
            this.Close();

        }
        private void LoadRoles()
        {
            using (var context = new Prngroup4Context())
            {
                var roles = context.Users
                    .Select(r => r.Role)
                    .Distinct()
                    .ToList();

                cmbRole.ItemsSource = roles; 
            }
        }

        private void cmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbRole.SelectedItem != null)
            {
                string selectedRole = cmbRole.SelectedItem.ToString();
                //MessageBox.Show("You selected Role: " + selectedRole);
            }

        }
    }
}

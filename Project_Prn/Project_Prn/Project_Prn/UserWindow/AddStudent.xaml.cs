using Project_Prn.dal;
using Project_Prn.Models;
using System.Windows;

namespace Project_Prn.UserWindow
{
    public partial class AddStudent : Window
    {
        private StudentManagement studentManagementWindow;

        public AddStudent(StudentManagement window)
        {
            InitializeComponent();
            studentManagementWindow = window;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text;
            string email = txtEmail.Text;
            string classes = txtClass.Text;
            string phone = txtPhone.Text;
            string school = txtSchool.Text;
            string pass = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(fullName))
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
                Role = "Student", // hardcoded role
                Password = pass
            };

            userDAO.AddUser(newUser);

            studentManagementWindow?.LoadUsers();

            this.Close();
        }
    }
}

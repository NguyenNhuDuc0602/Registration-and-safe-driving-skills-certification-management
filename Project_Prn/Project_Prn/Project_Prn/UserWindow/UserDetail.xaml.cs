using Project_Prn.dal;
using Project_Prn.Models;
using System;
using System.Windows;

namespace Project_Prn.UserWindow
{
    public partial class UserDetail : Window
    {
        private readonly User user;

        public UserDetail(User user)
        {
            InitializeComponent();
            this.user = user;
            LoadUserDetails();
        }

        private void LoadUserDetails()
        {
            txtFullName.Text = user.FullName;
            txtEmail.Text = user.Email;
            txtPhone.Text = user.Phone;
            txtClass.Text = user.Class;
            txtSchool.Text = user.School;
            cmbRole.SelectedValue = user.Role;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            user.FullName = txtFullName.Text;
            user.Email = txtEmail.Text;
            user.Phone = txtPhone.Text;
            user.Class = txtClass.Text;
            user.School = txtSchool.Text;
            user.Role = cmbRole.SelectedValue?.ToString();

            UserDAO userDAO = new UserDAO();
            userDAO.UpdateUser(user);

            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

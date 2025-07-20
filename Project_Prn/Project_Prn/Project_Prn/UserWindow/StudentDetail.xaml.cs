using Project_Prn.dal;
using Project_Prn.Models;
using System;
using System.Windows;

namespace Project_Prn.UserWindow
{
    public partial class StudentDetail : Window
    {
        private readonly User student;

        public StudentDetail(User student)
        {
            InitializeComponent();
            this.student = student;
            LoadStudentDetails();
        }

        private void LoadStudentDetails()
        {
            txtFullName.Text = student.FullName;
            txtEmail.Text = student.Email;
            txtPhone.Text = student.Phone;
            txtClass.Text = student.Class;
            txtSchool.Text = student.School;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            student.FullName = txtFullName.Text.Trim();
            student.Email = txtEmail.Text.Trim();
            student.Phone = txtPhone.Text.Trim();
            student.Class = txtClass.Text.Trim();
            student.School = txtSchool.Text.Trim();
            student.Role = "Student"; // Ensure role is student

            UserDAO dao = new UserDAO();
            dao.UpdateUser(student);

            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

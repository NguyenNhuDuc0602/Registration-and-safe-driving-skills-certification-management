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
    public partial class StudentProfileWindow : Window
    {
        private User currentUser;

        public StudentProfileWindow(User user)
        {
            InitializeComponent();
            currentUser = user;

            // Gán dữ liệu lên các textbox
            txtFullName.Text = currentUser.FullName;
            txtEmail.Text = currentUser.Email;
            txtSchool.Text = currentUser.School;
            txtClass.Text = currentUser.Class;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using var context = new Prngroup4Context();
            var dbUser = context.Users.FirstOrDefault(u => u.UserId == currentUser.UserId);

            if (dbUser != null)
            {
                dbUser.FullName = txtFullName.Text.Trim();
                dbUser.Email = txtEmail.Text.Trim();
                dbUser.School = txtSchool.Text.Trim();
                dbUser.Class = txtClass.Text.Trim();
                context.SaveChanges();

                // Đồng bộ lại thông tin trong đối tượng truyền vào
                currentUser.FullName = dbUser.FullName;
                currentUser.Email = dbUser.Email;
                currentUser.School = dbUser.School;
                currentUser.Class = dbUser.Class;

                MessageBox.Show("Cập nhật thành công", "Thông báo");
                this.DialogResult = true;
                this.Close();
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
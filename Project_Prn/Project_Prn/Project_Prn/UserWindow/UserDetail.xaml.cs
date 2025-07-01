using Project_Prn.Models;
using Project_Prn.dal;
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
    /// Interaction logic for UserDetail.xaml
    /// </summary>
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {        
            this.Close(); 
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            user.FullName = txtFullName.Text;
            user.Email = txtEmail.Text;
            user.Phone = txtPhone.Text;
            user.Class = txtClass.Text;
            user.School = txtSchool.Text;
            user.Role = cmbRole.SelectedValue.ToString();

            var userDAO = new UserDAO();
            userDAO.UpdateUser(user); // Cập nhật thông tin người dùng trong cơ sở dữ liệu
            this.DialogResult = true; // Đặt kết quả của cửa sổ là true để thông báo cập nhật thành công
            Close(); // Đóng cửa sổ chi tiết người dùng hiện tại
        }
    }
}

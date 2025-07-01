
using Microsoft.IdentityModel.Tokens;
using Project_Prn.dal;
using Project_Prn.Models;
using Project_Prn.UserWindow;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {

        public UserManagement()
        {
            InitializeComponent();
            LoadUsers();
            
        }
        
        public  void LoadUsers()
        {
            UserDAO userDAO = new UserDAO();
            var users =  userDAO.GetAllUser();
            this.dgUsers.ItemsSource = users;
        }
        
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUserWindow = new AddUser(this);
            addUserWindow.ShowDialog();

        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var selectUser = this.dgUsers.SelectedItem as User;
            if (selectUser == null)
            {
                MessageBox.Show("Vui lòng chọn một người dùng.");
                return;
            }
            var detailWindow = new UserDetail(selectUser);
            bool? updated = detailWindow.ShowDialog(); // tra ve true neu nut update duoc nhan
            if (updated == true)
            {
                LoadUsers(); // Cap nhat lai danh sach nguoi dung neu co thay doi
            }

        }
        public void LoadUsersByFullName(string name)
        {
            UserDAO userDAO = new UserDAO();
            var users = userDAO.GetByFullName(name);
            this.dgUsers.ItemsSource = users;
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if(!searchText.IsNullOrEmpty())
            {
                LoadUsersByFullName(searchText);
                if(dgUsers.AllowDrop == null || dgUsers.Items.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy người dùng nào với tên này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else             {
                LoadUsers(); // Neu khong co tu khoa tim kiem, tai lai danh sach nguoi dung
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = this.dgUsers.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn một người dùng để xóa.");
                return;
            }
            UserDAO userDAO = new UserDAO();
            userDAO.DeleteUser(selectedUser.UserId);
            LoadUsers(); // Cập nhật lại danh sách người dùng sau khi xóa
            MessageBox.Show("Người dùng đã được xóa thành công.");
        }
    }

}


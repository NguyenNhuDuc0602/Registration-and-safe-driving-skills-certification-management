using Project_Prn.dal;
using Project_Prn.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Project_Prn.UserWindow
{
    public partial class StudentManagement : Window
    {
        public StudentManagement()
        {
            InitializeComponent();
            LoadUsers();
        }

        public void LoadUsers()
        {
            UserDAO userDAO = new UserDAO();
            var users = userDAO.GetAllStudent();
            dgUsers.ItemsSource = users;
        }

        public void LoadUsersByFullName(string name)
        {
            UserDAO userDAO = new UserDAO();
            var users = userDAO.GetByFullName(name);
            dgUsers.ItemsSource = users;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUserWindow = new AddUser(this);
            addUserWindow.ShowDialog();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = dgUsers.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn một người dùng.");
                return;
            }

            var detailWindow = new UserDetail(selectedUser);
            bool? updated = detailWindow.ShowDialog();
            if (updated == true)
            {
                LoadUsers();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                LoadUsersByFullName(searchText);
                if (dgUsers.Items.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy người dùng nào với tên này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                LoadUsers();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = dgUsers.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn một người dùng để xóa.");
                return;
            }

            UserDAO userDAO = new UserDAO();
            userDAO.DeleteUser(selectedUser.UserId);
            LoadUsers();
            MessageBox.Show("Người dùng đã được xóa thành công.");
        }
    }
}

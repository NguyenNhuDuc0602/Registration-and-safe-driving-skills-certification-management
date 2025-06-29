
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
        // load information about users
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
    }

}


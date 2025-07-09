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
using Project_Prn.dal;
using Project_Prn.Models;


namespace Project_Prn.RegistrationWindow
{
    /// <summary>
    /// Interaction logic for AddRegistration.xaml
    /// </summary>
    public partial class AddRegistration : Window
    {
        private RegistrationManagement regisManagementWindow;
        public AddRegistration(RegistrationManagement regisManagementWindow)
        {
            InitializeComponent();
            this.regisManagementWindow = regisManagementWindow;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int UserID = Int32.Parse(txtUserID.Text);
            int CourseID = Int32.Parse(txtCourseID.Text);
            string Status = (cmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString();
            string Comments = txtComment.Text.Trim();

            RegistrationDAO registrationDAO = new RegistrationDAO();
            UserDAO userDAO = new UserDAO();

            if (!userDAO.IsUserExist(UserID))
            {
                MessageBox.Show($"User ID {UserID} does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CourseDAO courseDAO = new CourseDAO();

            if (!courseDAO.IsCourseExist(CourseID))
            {
                MessageBox.Show($"Course ID {UserID} does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Registration registration = new Registration
            {
                UserId = UserID,
                CourseId = CourseID,
                Status = Status,
                Comments = Comments
            };

            registrationDAO.AddRegistration(registration);
            regisManagementWindow.LoadRegistration();
            this.Close();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

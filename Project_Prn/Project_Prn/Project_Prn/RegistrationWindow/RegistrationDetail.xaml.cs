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
    /// Interaction logic for RegistrationDetail.xaml
    /// </summary>
    public partial class RegistrationDetail : Window
    {
        private readonly Registration registration;
        public RegistrationDetail(Registration registration)
        {
            InitializeComponent();
            this.registration = registration;
            LoadRegistrationDetail();
        }

        public void LoadRegistrationDetail()
        {
            txtRegisID.Text = registration.RegistrationId.ToString();
            txtUserID.Text = registration.UserId.ToString();
            txtCourseID.Text = registration.CourseId.ToString();
            cmbStatus.SelectedValue = registration.Status;
            txtComment.Text = registration.Comments;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Registration updatedRegistration = new Registration
            {
                RegistrationId = registration.RegistrationId,
                UserId = Int32.Parse(txtUserID.Text),
                CourseId = Int32.Parse(txtCourseID.Text),
                Status = (cmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Comments = txtComment.Text.Trim()
            };

            Prngroup4Context context = new Prngroup4Context();
            RegistrationDAO registrationsDAO = new RegistrationDAO(context);
            registrationsDAO.UpdateRegistration(updatedRegistration);

            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }
    }
}

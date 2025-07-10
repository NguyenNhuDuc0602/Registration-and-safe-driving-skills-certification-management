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

namespace Project_Prn.CertificateWindow
{
    /// <summary>
    /// Interaction logic for AddCertificate.xaml
    /// </summary>
    public partial class AddCertificate : Window
    {
        private CertificateManagement cerManagementWindow;
        public AddCertificate(CertificateManagement cerManagementWindow)
        {
            InitializeComponent();
            this.cerManagementWindow = cerManagementWindow;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int UserId = Int32.Parse(txtUserID.Text);
            DateOnly IssuedDate = DateOnly.FromDateTime(dpIssuedDate.SelectedDate.Value);
            DateOnly ExpirationDate = DateOnly.FromDateTime(dpExpirationDate.SelectedDate.Value);
            string CertificateCode = txtCerCode.Text.Trim();

            CertificateDAO certificateDAO = new CertificateDAO();
            UserDAO userDAO = new UserDAO();

            if (!userDAO.IsUserExist(UserId))
            {
                MessageBox.Show($"User ID {UserId} does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Certificate certificate = new Certificate
            {
                UserId = UserId,
                IssuedDate = IssuedDate,
                ExpirationDate = ExpirationDate,
                CertificateCode = CertificateCode
            };
            certificateDAO.AddCertificate(certificate);
            cerManagementWindow.LoadCertificate();
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

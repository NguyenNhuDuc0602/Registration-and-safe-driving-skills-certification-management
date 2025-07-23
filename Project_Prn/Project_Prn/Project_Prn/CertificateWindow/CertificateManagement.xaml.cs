using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System.Linq;
using System.Windows;

namespace Project_Prn.CertificateWindow
{
    public partial class CertificateManagement : Window
    {
        public CertificateManagement()
        {
            InitializeComponent();
            LoadCertificates();
        }

        private void LoadCertificates()
        {
            using var context = new Prngroup4Context();
            var certs = context.Certificates
                .Include(c => c.User)
                .Include(c => c.Course)
                .ToList();

            dgCertificate.ItemsSource = certs;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();
            using var context = new Prngroup4Context();
            var certs = context.Certificates
                .Include(c => c.User)
                .Include(c => c.Course)
                .Where(c => c.User.FullName.ToLower().Contains(keyword)
                         || c.CertificateCode.ToLower().Contains(keyword)
                         || c.Course.CourseName.ToLower().Contains(keyword))
                .ToList();

            dgCertificate.ItemsSource = certs;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            if (dgCertificate.SelectedItem is Certificate selected)
            {
                MessageBox.Show($"Certificate Details:\n\n" +
                    $"Name: {selected.User.FullName}\n" +
                    $"Course: {selected.Course.CourseName}\n" +
                    $"Issued: {selected.IssuedDate}\n" +
                    $"Expires: {selected.ExpirationDate}\n" +
                    $"Code: {selected.CertificateCode}", "Detail");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgCertificate.SelectedItem is Certificate selected)
            {
                var confirm = MessageBox.Show("Are you sure to delete this certificate?", "Confirm", MessageBoxButton.YesNo);
                if (confirm == MessageBoxResult.Yes)
                {
                    using var context = new Prngroup4Context();
                    var cert = context.Certificates.FirstOrDefault(c => c.CertificateId == selected.CertificateId);
                    if (cert != null)
                    {
                        context.Certificates.Remove(cert);
                        context.SaveChanges();
                        MessageBox.Show("Deleted successfully!");
                        LoadCertificates();
                    }
                }
            }
        }
    }
}

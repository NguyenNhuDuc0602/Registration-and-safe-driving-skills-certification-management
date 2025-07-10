using System;
using System.Collections.Generic;
using System.Windows;
using Project_Prn.dal;
using Project_Prn.Models;

namespace Project_Prn.CertificateWindow
{
    /// <summary>
    /// Interaction logic for CertificateManagement.xaml
    /// </summary>
    public partial class CertificateManagement : Window
    {
        public CertificateManagement()
        {
            InitializeComponent();
            LoadCertificate();
        }

        public void LoadCertificate()
        {
            CertificateDAO dao = new CertificateDAO();
            var cer = dao.GetAllCertificate();
            this.dgCertificate.ItemsSource = cer;
        }

        private void btnAddCertificate_Click(object sender, RoutedEventArgs e)
        {
            AddCertificate addCerWindow = new AddCertificate(this);
            addCerWindow.ShowDialog();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var selectCer = this.dgCertificate.SelectedItem as Certificate;
            if (selectCer == null)
            {
                MessageBox.Show("Vui lòng chọn 1 chứng chỉ!");
                return;
            }

            var detailWindow = new CertificateDetail(selectCer);
            bool? updated = detailWindow.ShowDialog();
            if (updated == true)
            {
                LoadCertificate();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedCer = this.dgCertificate.SelectedItem as Certificate;
            if (selectedCer == null)
            {
                MessageBox.Show("Vui lòng chọn một chứng chỉ để xóa.");
                return;
            }

            CertificateDAO certificateDAO = new CertificateDAO();
            certificateDAO.DeleteCertificate(selectedCer.CertificateId);
            LoadCertificate();
            MessageBox.Show("Xóa chứng chỉ thành công!");
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string input = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int cerId))
            {
                MessageBox.Show("Vui lòng nhập CerID hợp lệ!!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CertificateDAO certDAO = new CertificateDAO();
            var cer = certDAO.GetByIdCertificate(cerId);

            if (cer != null)
            {
                dgCertificate.ItemsSource = new List<Certificate> { cer };
            }
            else
            {
                MessageBox.Show($"Không tìm thấy chứng chỉ nào có CerID là {cerId}!!", "Thông Báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                dgCertificate.ItemsSource = null;
            }
        }
    }
}

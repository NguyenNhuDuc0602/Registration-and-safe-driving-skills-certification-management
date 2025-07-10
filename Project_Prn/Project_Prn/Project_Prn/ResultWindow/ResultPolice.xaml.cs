using Project_Prn.dal;
using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Project_Prn.ResultWindow
{
    public partial class ResultPolice : Window
    {
        private ResultDAO resultDAO = new ResultDAO();
        private CertificateDAO certificateDAO = new CertificateDAO();

        public ResultPolice()
        {
            InitializeComponent();
            LoadResults();
        }

        private void LoadResults()
        {
            var resultList = resultDAO.GetAllResult();
            dgResults.ItemsSource = resultList;

        }

        private void BtnIssueCertificate_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.DataContext is Result selectedResult)
            {
                // Kiểm tra xem học viên đã có chứng chỉ chưa
                var existingCertificates = certificateDAO.GetByUserIdCertificate(selectedResult.UserId);
                if (existingCertificates.Any())
                {
                    MessageBox.Show("Học viên này đã có chứng chỉ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Cấp chứng chỉ mới
                Certificate newCertificate = new Certificate
                {
                    UserId = selectedResult.UserId,
                    IssuedDate = DateOnly.FromDateTime(DateTime.Now),
                    ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(5)),
                    CertificateCode = GenerateCertificateCode(selectedResult.UserId)
                };

                certificateDAO.AddCertificate(newCertificate);
                MessageBox.Show("Cấp chứng chỉ thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private string GenerateCertificateCode(int userId)
        {
            // Ví dụ mã chứng chỉ: CERT-2025-00123
            return $"CERT-{DateTime.Now.Year}-{userId.ToString("D5")}";
        }
    }
}

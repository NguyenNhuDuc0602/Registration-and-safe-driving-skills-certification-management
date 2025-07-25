﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
using Project_Prn.CertificateWindow;
using Project_Prn.dal;
using Project_Prn.Models;

namespace Project_Prn.RegistrationWindow
{
    /// <summary>
    /// Interaction logic for RegistrationManagement.xaml
    /// </summary>
    public partial class RegistrationManagement : Window
    {
        public RegistrationManagement()
        {
            InitializeComponent();
            LoadRegistration();
        }
        public void LoadRegistration()
        {
            Prngroup4Context context = new Prngroup4Context();
            RegistrationDAO dao = new RegistrationDAO(context);
            var regis = dao.GetAllRegistration();
            this.dgRegistration.ItemsSource = regis;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var selectedRegis = this.dgRegistration.SelectedItem as Registration;
            if (selectedRegis == null)
            {
                MessageBox.Show("Please choose the Registration!!!");
                return;
            }

            var detailWindow = new RegistrationDetail(selectedRegis);
            bool? updated = detailWindow.ShowDialog();
            if (updated == true)
            {
                LoadRegistration(); 
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedRegis = this.dgRegistration.SelectedItem as Registration;
            if (selectedRegis == null)
            {
                MessageBox.Show("Please choose the Registration!!!");
                return;
            }
            Prngroup4Context context = new Prngroup4Context();
            RegistrationDAO registrationDAO = new RegistrationDAO(context);
            registrationDAO.DeleteRegistration(selectedRegis.RegistrationId);
            LoadRegistration();
            MessageBox.Show("Completed!");
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string input = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int regisID)) {
                MessageBox.Show("Vui lòng nhập RegisID hợp lệ!!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Prngroup4Context context = new Prngroup4Context();
            RegistrationDAO regisDAO = new RegistrationDAO(context);
            var regis = regisDAO.GetByIdRegistration(regisID);

            if (regis !=null)
            {
                dgRegistration.ItemsSource = new List<Registration> { regis};
            }
            else
            {
                MessageBox.Show($"Không tìm thấy Registration nào có regisID là {regisID}!!", "Thông Báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                dgRegistration.ItemsSource = null;
            }
        }
        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgRegistration.SelectedItem as Registration;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn đơn đăng ký để duyệt!");
                return;
            }

            Prngroup4Context context = new Prngroup4Context();
            RegistrationDAO dao = new RegistrationDAO(context);
            dao.UpdateRegistrationStatus(selected.RegistrationId, "Approved");
            LoadRegistration();

            MessageBox.Show("Đơn đăng ký đã được duyệt!");
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgRegistration.SelectedItem as Registration;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn đơn đăng ký để từ chối!");
                return;
            }

            // Hiển thị hộp thoại để nhập lý do từ chối
            string comment = Microsoft.VisualBasic.Interaction.InputBox(
                "Vui lòng nhập lý do từ chối:", "Từ chối đơn đăng ký", "");

            if (string.IsNullOrWhiteSpace(comment))
            {
                MessageBox.Show("Bạn cần nhập lý do từ chối!");
                return;
            }

            Prngroup4Context context = new Prngroup4Context();
            RegistrationDAO dao = new RegistrationDAO(context);
            dao.RejectRegistrationWithComment(selected.RegistrationId, comment);
            LoadRegistration();
            MessageBox.Show("Đơn đăng ký đã bị từ chối!");
        }
    }
}

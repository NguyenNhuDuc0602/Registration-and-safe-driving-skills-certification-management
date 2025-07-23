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
using Microsoft.VisualBasic.ApplicationServices;
using Project_Prn.dal;
using Project_Prn.Models;

namespace Project_Prn.CertificateWindow
{
    /// <summary>
    /// Interaction logic for CertificateDetail.xaml
    /// </summary>
    public partial class CertificateDetail : Window
    {
        private readonly Certificate certificate;
        public CertificateDetail(Certificate certificate)
        {
            InitializeComponent();
            this.certificate = certificate;
            LoadCertificateDetail();
        }

        private void LoadCertificateDetail()
        {
            // Load all courses
            using var context = new Prngroup4Context();
            var courses = context.Courses.ToList();
            cbxCourse.ItemsSource = courses;

            // Load data from certificate
            txtUserID.Text = certificate.UserId.ToString();
            dpIssuedDate.SelectedDate = certificate.IssuedDate.ToDateTime(TimeOnly.MinValue);
            dpExpirationDate.SelectedDate = certificate.ExpirationDate.ToDateTime(TimeOnly.MinValue);
            txtCerCode.Text = certificate.CertificateCode;

            cbxCourse.SelectedValue = certificate.CourseId;
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (cbxCourse.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn môn học!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Certificate updatedCertificate = new Certificate
            {
                CertificateId = certificate.CertificateId,
                UserId = int.Parse(txtUserID.Text),
                CourseId = (int)cbxCourse.SelectedValue,
                IssuedDate = DateOnly.FromDateTime(dpIssuedDate.SelectedDate.Value),
                ExpirationDate = DateOnly.FromDateTime(dpExpirationDate.SelectedDate.Value),
                CertificateCode = txtCerCode.Text.Trim()
            };

            CertificateDAO certificateDAO = new CertificateDAO();
            certificateDAO.UpdateCertificate(updatedCertificate);

            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }

    }
}

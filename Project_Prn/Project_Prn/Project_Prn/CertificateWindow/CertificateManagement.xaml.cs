using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using Project_Prn.Models;
using System.Diagnostics;
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
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (dgCertificate.SelectedItem is Certificate selected)
            {
                using (var context = new Prngroup4Context())
                {
                    var cert = context.Certificates
                        .Include(c => c.User)
                        .Include(c => c.Course)
                        .FirstOrDefault(c => c.CertificateId == selected.CertificateId);

                    if (cert == null)
                    {
                        MessageBox.Show("Không tìm thấy chứng chỉ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    PdfDocument document = new PdfDocument();
                    document.Info.Title = "Certificate";
                    PdfPage page = document.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    // Font settings
                    XFont titleFont = new XFont("Georgia", 26, XFontStyle.Bold);
                    XFont subtitleFont = new XFont("Georgia", 16, XFontStyle.BoldItalic);
                    XFont bodyFont = new XFont("Georgia", 14);
                    XBrush brush = XBrushes.Black;

                    // Draw border
                    gfx.DrawRectangle(XPens.DarkBlue, new XRect(40, 40, page.Width - 80, page.Height - 80));

                    // Draw title
                    gfx.DrawString("CERTIFICATE OF COMPLETION", titleFont, XBrushes.DarkBlue,
                        new XRect(0, 80, page.Width, 40), XStringFormats.TopCenter);

                    // Draw content
                    gfx.DrawString($"This is to certify that", bodyFont, brush,
                        new XRect(0, 140, page.Width, 30), XStringFormats.TopCenter);

                    gfx.DrawString($"{cert.User.FullName}", subtitleFont, XBrushes.DarkGreen,
                        new XRect(0, 180, page.Width, 30), XStringFormats.TopCenter);

                    gfx.DrawString($"has successfully completed the course", bodyFont, brush,
                        new XRect(0, 220, page.Width, 30), XStringFormats.TopCenter);

                    gfx.DrawString($"\"{cert.Course?.CourseName ?? "N/A"}\"", subtitleFont, brush,
                        new XRect(0, 260, page.Width, 30), XStringFormats.TopCenter);

                    gfx.DrawString($"Certificate Code: {cert.CertificateCode}", bodyFont, XBrushes.Gray,
                        new XRect(0, 320, page.Width, 20), XStringFormats.TopCenter);

                    gfx.DrawString($"Issued on: {cert.IssuedDate:dd/MM/yyyy}", bodyFont, brush,
                        new XRect(0, 350, page.Width, 20), XStringFormats.TopCenter);

                    gfx.DrawString($"Expires on: {cert.ExpirationDate:dd/MM/yyyy}", bodyFont, brush,
                        new XRect(0, 380, page.Width, 20), XStringFormats.TopCenter);

                    // Optional: Signature placeholder
                    gfx.DrawString("Instructor's Signature", bodyFont, brush,
                        new XRect(page.Width - 220, page.Height - 100, 200, 20), XStringFormats.TopLeft);
                    gfx.DrawLine(XPens.Black, page.Width - 220, page.Height - 80, page.Width - 40, page.Height - 80);

                    string filename = $"Certificate_{cert.User.FullName}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                    document.Save(filename);
                    Process.Start("explorer.exe", filename);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chứng chỉ để xuất.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}

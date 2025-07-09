using Project_Prn.dal;
using Project_Prn.Models;
using System;
using System.Linq;
using System.Windows;

namespace Project_Prn.ResultWindow
{
    public partial class StudentResult : Window
    {
        public StudentResult() : this(123)
        {
        }

        public StudentResult(int userId)
        {
            InitializeComponent();
            LoadStudentResults(userId);
            ShowCertificateInfo(userId);
        }

        private void LoadStudentResults(int userId)
        {
            var resultList = ResultDAO.GetByUserIdResult(userId);

            var displayResults = resultList.Select(r => new
            {
                ExamName = $"Exam {r.Exam.ExamId}",
                CourseName = r.Exam.Course.CourseName,
                ExamDate = r.Exam.Date.ToString("yyyy-MM-dd"),
                Score = r.Score,
                Status = r.PassStatus ? "Passed" : "Failed",
                TeacherName = r.Exam.Course.Teacher.FullName
            }).ToList();

            ResultGrid.ItemsSource = displayResults;
        }

        private void ShowCertificateInfo(int userId)
        {
            var dao = new CertificateDAO();
            var certificates = dao.GetByUserIdCertificate(userId);
            var certificate = certificates.FirstOrDefault();

            if (certificate != null)
            {
                CertificateTextBlock.Text = "🎉 Bạn đã đạt chứng chỉ!";
                CertificateTextBlock.Visibility = Visibility.Visible;
                CertificateInfoPanel.Visibility = Visibility.Visible;

                // Gán trực tiếp không dùng DataContext
                CertificateCodeText.Text = $"Mã chứng chỉ: {certificate.CertificateCode}";
                IssueDateText.Text = $"Ngày cấp: {certificate.IssuedDate.ToString("yyyy-MM-dd")}";
                ExpiryDateText.Text = $"Ngày hết hạn: {certificate.ExpirationDate.ToString("yyyy-MM-dd")}";
            }
            else
            {
                CertificateTextBlock.Visibility = Visibility.Collapsed;
                CertificateInfoPanel.Visibility = Visibility.Collapsed;
            }
        }

    }
}

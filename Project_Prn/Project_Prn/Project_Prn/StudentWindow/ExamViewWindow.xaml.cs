using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Project_Prn.StudentWindow
{
    public partial class ExamViewWindow : Window
    {
        private readonly User currentUser;

        public ExamViewWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadExams();
        }

        private void LoadExams()
        {
            using var context = new Prngroup4Context();
            var today = DateTime.Today;

            var exams = context.Registrations
                .Where(r => r.UserId == currentUser.UserId && r.Status == "Approved")
                .Include(r => r.Course)
                    .ThenInclude(c => c.Exams)
                .SelectMany(r => r.Course.Exams)
                .Include(e => e.Course)
                .Where(e =>
                    e.ExamDate.Date == today &&
                    !context.ExamResults.Any(er => er.UserId == currentUser.UserId && er.ExamId == e.ExamId))
                .ToList();

            dgExams.ItemsSource = exams;
        }

        private void TakeExam_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int examId)
            {
                using var context = new Prngroup4Context();
                var exam = context.Exams.Find(examId);

                if (exam == null)
                {
                    MessageBox.Show("Không tìm thấy kỳ thi.");
                    return;
                }

                if (exam.ExamDate.Date != DateTime.Today)
                {
                    MessageBox.Show("Hiện tại chưa đến ngày thi.");
                    return;
                }

                var examWindow = new ExamTakingWindow(examId, currentUser.UserId);
                examWindow.ShowDialog();

                LoadExams(); // Cập nhật lại sau khi làm xong
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

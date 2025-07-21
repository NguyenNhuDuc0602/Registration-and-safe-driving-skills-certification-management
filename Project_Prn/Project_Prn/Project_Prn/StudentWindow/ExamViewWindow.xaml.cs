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

            var exams = context.Registrations
                .Where(r => r.UserId == currentUser.UserId && r.Status == "Approved")
                .Include(r => r.Course)
                    .ThenInclude(c => c.Exams)
                .SelectMany(r => r.Course.Exams)
                .Include(e => e.Course)
                .Where(e => !context.Results.Any(r => r.UserId == currentUser.UserId && r.ExamId == e.ExamId))
                .ToList();

            dgExams.ItemsSource = exams;
        }


        private void TakeExam_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int examId)
            {
                using var context = new Prngroup4Context();
                var exam = context.Exams
                    .Include(e => e.Course)
                    .FirstOrDefault(e => e.ExamId == examId);

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

                // Kiểm tra điều kiện điểm danh
                int presentCount = context.Attendances
                    .Count(a => a.CourseId == exam.CourseId && a.UserId == currentUser.UserId && a.Status == "Present");

                // Tính tổng số buổi học đã được điểm danh (buổi học với trạng thái "Present" hoặc "Absent")
                int totalSessions = context.Attendances
                    .Count(a => a.CourseId == exam.CourseId && a.UserId == currentUser.UserId);

                // Tính tỷ lệ điểm danh
                double attendanceRate = totalSessions == 0 ? 0 : (presentCount * 100.0 / totalSessions);

                if (attendanceRate < 80)
                {
                    MessageBox.Show("Bạn không đủ điều kiện dự thi vì điểm danh dưới 80%.", "Không đủ điều kiện", MessageBoxButton.OK, MessageBoxImage.Warning);
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

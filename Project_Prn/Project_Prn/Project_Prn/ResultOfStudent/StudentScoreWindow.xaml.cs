using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System.Linq;
using System.Windows;

namespace Project_Prn.ResultOfStudent
{
    public partial class StudentScoreWindow : Window
    {
        private readonly User currentUser;

        public StudentScoreWindow(User teacher)
        {
            InitializeComponent();
            currentUser = teacher;
            LoadScores();
        }

        private void LoadScores()
        {
            using var context = new Prngroup4Context();

            var results = context.Results
                .Where(r => r.Exam.Course.TeacherId == currentUser.UserId)
                .Include(r => r.User) // Load tên sinh viên
                .Include(r => r.Exam)
                    .ThenInclude(e => e.Course) // Load tên khóa học
                .OrderBy(r => r.User.FullName)
                .ToList();

            dgScores.ItemsSource = results;
        }

    }
}

using Project_Prn.Models;
using System;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace Project_Prn.StudentWindow
{
    public partial class StudentResultWindow : Window
    {
        private readonly User currentUser;
        private readonly Prngroup4Context context = new();

        public StudentResultWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadResults();
        }

        private void LoadResults()
        {
            var results = context.Results
               .Where(r => r.UserId == currentUser.UserId)
               .Include(r => r.Exam)
               .ThenInclude(e => e.Course)
               .Select(r => new
               {
                   CourseName = r.Exam.Course.CourseName,
                   ExamDate = r.Exam.Date.ToString("dd/MM/yyyy"),
                   Score = (double)r.Score, 
                   PassStatus = r.PassStatus ? "✔️" : "❌"
               })
               .ToList();

            resultDataGrid.ItemsSource = results;
        }
    }
}

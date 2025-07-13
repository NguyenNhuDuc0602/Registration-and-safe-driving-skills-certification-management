using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
                .Include(e => e.Course) // Quan trọng!
                .ToList();

            dgExams.ItemsSource = exams;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

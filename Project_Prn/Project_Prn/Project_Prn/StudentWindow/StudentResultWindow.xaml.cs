using Project_Prn.Models;
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
                    r.Score,
                    PassStatus = r.PassStatus ? "✔️" : "❌"
                })
                .ToList();

            resultDataGrid.ItemsSource = results;
        }
    }
}
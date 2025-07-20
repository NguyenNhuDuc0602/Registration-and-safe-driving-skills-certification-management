using Project_Prn.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Project_Prn.QuestionWindow
{
    public partial class QuestionBankWindow : Window
    {
        private readonly User currentUser;

        public QuestionBankWindow(User user) // Constructor có 1 tham số
        {
            InitializeComponent();
            currentUser = user;
            LoadQuestions(); // gọi hàm load nếu cần
        }

        private void LoadQuestions()
        {
            using (var context = new Prngroup4Context())
            {
                var questions = context.Questions.ToList();
                dgQuestions.ItemsSource = questions;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var window = new QuestionEditWindow();
            if (window.ShowDialog() == true)
            {
                using (var context = new Prngroup4Context())
                {
                    context.Questions.Add(window.Question);  // Lưu câu hỏi mới
                    context.SaveChanges();
                }
                LoadQuestions(); // Refresh DataGrid
            }
        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgQuestions.SelectedItem is Question selectedQuestion)
            {
                var window = new QuestionEditWindow(selectedQuestion); 
                if (window.ShowDialog() == true)
                {
                    LoadQuestions();
                }
            }
            else
            {
                MessageBox.Show("Please select a question to edit.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgQuestions.SelectedItem is Question selectedQuestion)
            {
                var result = MessageBox.Show("Are you sure you want to delete this question?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    using var context = new Prngroup4Context();
                    var questionToDelete = context.Questions.Find(selectedQuestion.QuestionId);
                    if (questionToDelete != null)
                    {
                        context.Questions.Remove(questionToDelete);
                        context.SaveChanges();
                        LoadQuestions();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a question to delete.");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

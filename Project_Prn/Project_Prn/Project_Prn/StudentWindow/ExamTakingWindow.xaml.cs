using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Project_Prn.StudentWindow
{
    public partial class ExamTakingWindow : Window
    {
        private readonly int _examId;
        private readonly int _userId;
        private List<Question> _questions;
        private int _currentIndex = 0;
        private Dictionary<int, string> _selectedAnswers = new();

        public ExamTakingWindow(int examId, int userId)
        {
            InitializeComponent();
            _examId = examId;
            _userId = userId;
            LoadQuestions();
            if (_questions != null && _questions.Count > 0)
            {
                ShowQuestion(_currentIndex);
            }
            else
            {
                MessageBox.Show("Không có câu hỏi nào cho bài thi này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                Close();
            }
        }

        private void LoadQuestions()
        {
            using var context = new Prngroup4Context();
            _questions = context.Questions
                .Where(q => q.ExamId == _examId)
                .OrderBy(q => q.QuestionId)
                .ToList();
        }

        private void ShowQuestion(int index)
        {
            if (_questions == null || index < 0 || index >= _questions.Count)
                return;

            var question = _questions[index];
            QuestionTextBlock.Text = $"Câu {index + 1}: {question.Content}";
            OptionARadio.Content = $"A. {question.OptionA}";
            OptionBRadio.Content = $"B. {question.OptionB}";
            OptionCRadio.Content = $"C. {question.OptionC}";
            OptionDRadio.Content = $"D. {question.OptionD}";

            // Reset selection
            OptionARadio.IsChecked = false;
            OptionBRadio.IsChecked = false;
            OptionCRadio.IsChecked = false;
            OptionDRadio.IsChecked = false;

            // Restore selected answer
            if (_selectedAnswers.TryGetValue(question.QuestionId, out var selected))
            {
                switch (selected)
                {
                    case "A": OptionARadio.IsChecked = true; break;
                    case "B": OptionBRadio.IsChecked = true; break;
                    case "C": OptionCRadio.IsChecked = true; break;
                    case "D": OptionDRadio.IsChecked = true; break;
                }
            }
        }

        private void SaveAnswer()
        {
            if (_currentIndex < 0 || _currentIndex >= _questions.Count)
                return;

            var questionId = _questions[_currentIndex].QuestionId;

            if (OptionARadio.IsChecked == true) _selectedAnswers[questionId] = "A";
            else if (OptionBRadio.IsChecked == true) _selectedAnswers[questionId] = "B";
            else if (OptionCRadio.IsChecked == true) _selectedAnswers[questionId] = "C";
            else if (OptionDRadio.IsChecked == true) _selectedAnswers[questionId] = "D";
        }

        private void PreviousQuestion_Click(object sender, RoutedEventArgs e)
        {
            SaveAnswer();
            if (_currentIndex > 0)
            {
                _currentIndex--;
                ShowQuestion(_currentIndex);
            }
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            SaveAnswer();
            if (_currentIndex < _questions.Count - 1)
            {
                _currentIndex++;
                ShowQuestion(_currentIndex);
            }
        }

        private void SubmitExam_Click(object sender, RoutedEventArgs e)
        {
            SaveAnswer();

            int correct = 0;
            foreach (var question in _questions)
            {
                if (_selectedAnswers.TryGetValue(question.QuestionId, out var selectedAnswer) &&
                    selectedAnswer == question.CorrectAnswer)
                {
                    correct++;
                }
            }

            double score = (double)correct / _questions.Count * 10;

            using var context = new Prngroup4Context();
            var result = new Result
            {
                ExamId = _examId,
                UserId = _userId,
                Score = (double?)(decimal)score,
                SubmittedAt = DateTime.Now,
                PassStatus = score >= 5
            };
            context.Results.Add(result);

            // CẤP CHỨNG CHỈ NẾU ĐẬU
            if (result.PassStatus)
            {
                // Lấy Exam để biết CourseId
                var exam = context.Exams.FirstOrDefault(e => e.ExamId == _examId);
                if (exam != null)
                {
                    int courseId = exam.CourseId;

                    // Kiểm tra nếu chưa có chứng chỉ cho user + course này
                    bool alreadyHasCertificate = context.Certificates
                        .Any(c => c.UserId == _userId && c.CourseId == courseId);

                    if (!alreadyHasCertificate)
                    {
                        var certificate = new Certificate
                        {
                            UserId = _userId,
                            CourseId = courseId,
                            IssuedDate = DateOnly.FromDateTime(DateTime.Now),
                            ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
                            CertificateCode = $"CERT-{_userId}-{DateTime.Now:yyyyMMddHHmmss}"
                        };
                        context.Certificates.Add(certificate);
                    }
                }

            }

            context.SaveChanges();

            MessageBox.Show($"Bạn đã nộp bài. Số điểm: {score:F2}", "Hoàn thành", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }


    }
}

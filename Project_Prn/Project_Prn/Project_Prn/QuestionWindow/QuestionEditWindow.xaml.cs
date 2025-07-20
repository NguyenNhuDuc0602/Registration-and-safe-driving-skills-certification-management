using Project_Prn.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Project_Prn.QuestionWindow
{
    public partial class QuestionEditWindow : Window
    {
        public Question Question { get; private set; }

        public QuestionEditWindow(Question question = null)
        {
            InitializeComponent();
            LoadExams();

            if (question == null)
            {
                Question = new Question();
            }
            else
            {
                Question = new Question
                {
                    QuestionId = question.QuestionId,
                    ExamId = question.ExamId,
                    Content = question.Content,
                    OptionA = question.OptionA,
                    OptionB = question.OptionB,
                    OptionC = question.OptionC,
                    OptionD = question.OptionD,
                    CorrectAnswer = question.CorrectAnswer
                };

                txtContent.Text = Question.Content;
                txtA.Text = Question.OptionA;
                txtB.Text = Question.OptionB;
                txtC.Text = Question.OptionC;
                txtD.Text = Question.OptionD;
                txtCorrect.Text = Question.CorrectAnswer;
                cbExams.SelectedValue = question.ExamId; // hiển thị Exam cũ
            }
        }

        private void LoadExams()
        {
            using var context = new Prngroup4Context();

            var exams = context.Exams
                .Select(e => new
                {
                    e.ExamId,
                    Title = e.Course.CourseName  
                })
                .ToList();

            cbExams.ItemsSource = exams;
            cbExams.DisplayMemberPath = "Title";
            cbExams.SelectedValuePath = "ExamId";
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContent.Text) ||
                string.IsNullOrWhiteSpace(txtA.Text) ||
                string.IsNullOrWhiteSpace(txtB.Text) ||
                string.IsNullOrWhiteSpace(txtC.Text) ||
                string.IsNullOrWhiteSpace(txtD.Text) ||
                string.IsNullOrWhiteSpace(txtCorrect.Text) ||
                cbExams.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin và chọn kỳ thi!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!"ABCD".Contains(txtCorrect.Text.ToUpper()))
            {
                MessageBox.Show("Đáp án đúng phải là một trong A, B, C hoặc D!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Question.Content = txtContent.Text.Trim();
            Question.OptionA = txtA.Text.Trim();
            Question.OptionB = txtB.Text.Trim();
            Question.OptionC = txtC.Text.Trim();
            Question.OptionD = txtD.Text.Trim();
            Question.CorrectAnswer = txtCorrect.Text.ToUpper().Trim();
            Question.ExamId = (int)cbExams.SelectedValue;

            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

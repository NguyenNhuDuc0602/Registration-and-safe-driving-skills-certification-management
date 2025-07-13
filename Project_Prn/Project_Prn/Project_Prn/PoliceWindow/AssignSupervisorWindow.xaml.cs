using Microsoft.EntityFrameworkCore;
using Project_Prn.dal;
using Project_Prn.Models;
using System.Linq;
using System.Windows;

namespace Project_Prn.PoliceWindow
{
    public partial class AssignSupervisorWindow : Window
    {
        private readonly Prngroup4Context context = new Prngroup4Context();
        private readonly ExamDAO examDAO = new ExamDAO();

        public AssignSupervisorWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // B1: Lấy danh sách kỳ thi chưa có giám thị (đã include Course)
            var rawExams = context.Exams
                .Where(e => e.SupervisorId == null)
                .Include(e => e.Course)
                .ToList(); // <-- Include xong mới gọi Select

            // B2: Tạo danh sách hiển thị
            var exams = rawExams.Select(e => new
            {
                e.ExamId,
                Display = $"{e.Course.CourseName} - {e.Room} - {e.Date:dd/MM/yyyy}"
            }).ToList();

            cbExam.ItemsSource = exams;
            cbExam.DisplayMemberPath = "Display";
            cbExam.SelectedValuePath = "ExamId";

            // Load teachers
            var teachers = context.Users
                .Where(u => u.Role == "Teacher")
                .ToList();

            cbSupervisor.ItemsSource = teachers;
            cbSupervisor.DisplayMemberPath = "FullName";
            cbSupervisor.SelectedValuePath = "UserId";
        }



        private void Assign_Click(object sender, RoutedEventArgs e)
        {
            if (cbExam.SelectedValue == null || cbSupervisor.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ kỳ thi và giám thị.");
                return;
            }

            int examId = (int)cbExam.SelectedValue;
            int teacherId = (int)cbSupervisor.SelectedValue;

            var exam = context.Exams.FirstOrDefault(e => e.ExamId == examId);
            if (exam != null)
            {
                exam.SupervisorId = teacherId;
                context.SaveChanges();

                MessageBox.Show("Phân công giám thị thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Không tìm thấy kỳ thi.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

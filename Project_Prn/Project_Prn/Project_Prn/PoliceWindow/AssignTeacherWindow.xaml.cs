using Project_Prn.Models;
using System.Linq;
using System.Windows;

namespace Project_Prn.PoliceWindow
{
    public partial class AssignTeacherWindow : Window
    {
        private readonly Prngroup4Context context = new Prngroup4Context();

        public AssignTeacherWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Lấy tất cả khóa học 
            var allCourses = context.Courses.ToList();
            cbClass.ItemsSource = allCourses;
            cbClass.DisplayMemberPath = "CourseName";
            cbClass.SelectedValuePath = "CourseId";

            // Lấy danh sách giáo viên
            var teachers = context.Users
                .Where(u => u.Role == "Teacher")
                .ToList();
            cbTeacher.ItemsSource = teachers;
            cbTeacher.DisplayMemberPath = "FullName";
            cbTeacher.SelectedValuePath = "UserId";
        }


        private void Assign_Click(object sender, RoutedEventArgs e)
        {
            if (cbClass.SelectedValue == null || cbTeacher.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ khóa học và giáo viên.");
                return;
            }

            int courseId = (int)cbClass.SelectedValue;
            int teacherId = (int)cbTeacher.SelectedValue;

            var course = context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course != null)
            {
                course.TeacherId = teacherId;
                context.SaveChanges();

                MessageBox.Show("Phân công thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Không tìm thấy khóa học.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Prn.dal
{
    public class CourseDAO
    {
        private Prngroup4Context dbc;
        public CourseDAO() { dbc = new Prngroup4Context(); }

        // 1. Lấy tất cả khóa học
        public List<Course> GetAllCourse()
        {
            return dbc.Courses
                .Include(c => c.Teacher) // Eager load thông tin giảng viên
                .ToList();
        }

        // 2. Lấy khóa học theo ID
        public Course? GetByIdCourse(int courseId)
        {
            return dbc.Courses
                .Include(c => c.Teacher) // Eager load thông tin giảng viên
                .FirstOrDefault(c => c.CourseId == courseId);
        }

        // 3. Thêm mới khóa học
        public void AddCourse(Course course)
        {
            dbc.Courses.Add(course);
            dbc.SaveChanges();
        }

        // 4. Cập nhật khóa học
        public void UpdateCourse(Course course)
        {
            dbc.Courses.Update(course);
            dbc.SaveChanges();
        }

        // 5. Xóa khóa học
        public void DeleteCourse(int courseId)
        {
            var course = dbc.Courses.Find(courseId);
            if (course != null)
            {
                dbc.Courses.Remove(course);
                dbc.SaveChanges();
            }
        }

        // 6. Lấy khóa học theo giảng viên (TeacherId)
        public List<Course> GetByTeacherIdCourse(int teacherId)
        {
            return dbc.Courses
                .Where(c => c.TeacherId == teacherId)
                .Include(c => c.Teacher) // Eager load thông tin giảng viên
                .ToList();
        }
        public bool IsCourseExist(int courseId)
        {
            return dbc.Courses.Any(c => c.CourseId == courseId);
        }

    }
}

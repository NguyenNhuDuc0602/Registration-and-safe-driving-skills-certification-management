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
        public async Task<List<Course>> GetAllCourseAsync()
        {
            return await dbc.Courses
                .Include(c => c.Teacher) // Eager load thông tin giảng viên
                .ToListAsync();
        }

        // 2. Lấy khóa học theo ID
        public async Task<Course?> GetByIdCourseAsync(int courseId)
        {
            return await dbc.Courses
                .Include(c => c.Teacher) // Eager load thông tin giảng viên
                .FirstOrDefaultAsync(c => c.CourseId == courseId);
        }

        // 3. Thêm mới khóa học
        public async Task AddCourseAsync(Course course)
        {
            dbc.Courses.Add(course);
            await dbc.SaveChangesAsync();
        }

        // 4. Cập nhật khóa học
        public async Task UpdateCourseAsync(Course course)
        {
            dbc.Courses.Update(course);
            await dbc.SaveChangesAsync();
        }

        // 5. Xóa khóa học
        public async Task DeleteCourseAsync(int courseId)
        {
            var course = await dbc.Courses.FindAsync(courseId);
            if (course != null)
            {
                dbc.Courses.Remove(course);
                await dbc.SaveChangesAsync();
            }
        }

        // 6. Lấy khóa học theo giảng viên (TeacherId)
        public async Task<List<Course>> GetByTeacherIdCourseAsync(int teacherId)
        {
            return await dbc.Courses
                .Where(c => c.TeacherId == teacherId)
                .Include(c => c.Teacher) // Eager load thông tin giảng viên
                .ToListAsync();
        }

        public bool IsCourseExist(int courseId)
        {
            return dbc.Courses.Any(c => c.CourseId == courseId);
        }
    }
}

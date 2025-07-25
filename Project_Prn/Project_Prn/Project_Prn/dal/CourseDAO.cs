﻿using Microsoft.EntityFrameworkCore;
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
        public List<User> GetAllTeachers()
        {
            return dbc.Users
                .Where(u => u.Role == "Teacher") 
                .ToList();
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
            var course = dbc.Courses
                .Include(c => c.Registrations)
                .Include(c => c.Exams)
                    .ThenInclude(e => e.Results)
                .FirstOrDefault(c => c.CourseId == courseId);

            if (course != null)
            {
                // Xóa kết quả thi trước
                foreach (var exam in course.Exams)
                {
                    dbc.Results.RemoveRange(exam.Results);
                }

                // Xóa kỳ thi
                dbc.Exams.RemoveRange(course.Exams);

                // Xóa đăng ký học
                dbc.Registrations.RemoveRange(course.Registrations);

                // Xóa khóa học
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
        // serach 
        public List<Course> GetCourseBylName(string name)
        {
            return dbc.Courses
                      .Where(c => c.CourseName.ToLower().Contains(name.ToLower()))
                      .Include(c => c.Teacher)
                      .ToList();
        }


    }
}

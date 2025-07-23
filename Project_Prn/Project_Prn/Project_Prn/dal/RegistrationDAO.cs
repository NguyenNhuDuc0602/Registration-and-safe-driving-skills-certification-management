using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Prn.dal
{
    public class RegistrationDAO
    {
        private Prngroup4Context dbc;
        public RegistrationDAO(Prngroup4Context dbc)
        {
            dbc = new Prngroup4Context();
            this.dbc = dbc;
        }

        // 1. Lấy tất cả đăng ký
        public List<Registration> GetAllRegistration()
        {
            return dbc.Registrations
                .Include(r => r.User)  // Eager load thông tin người dùng
                .Include(r => r.Course) // Eager load thông tin khóa học
                .ToList();
        }

        // 2. Lấy đăng ký theo ID
        public Registration? GetByIdRegistration(int registrationId)
        {
            return dbc.Registrations
                .Include(r => r.User)
                .Include(r => r.Course)
                .FirstOrDefault(r => r.RegistrationId == registrationId);
        }

        // 3. Thêm mới đăng ký
        public void AddRegistration(Registration registration)
        {
            dbc.Registrations.Add(registration);
            dbc.SaveChanges();
        }

        // 4. Cập nhật đăng ký
        public void UpdateRegistration(Registration registration)
        {
            dbc.Registrations.Update(registration);
            dbc.SaveChanges();
        }

        // 5. Xóa đăng ký
        public void DeleteRegistration(int registrationId)
        {
            var registration = dbc.Registrations.Find(registrationId);
            if (registration != null)
            {
                dbc.Registrations.Remove(registration);
                dbc.SaveChanges();
            }
        }

        // 6. Lấy đăng ký theo UserId
        public List<Registration> GetByUserIdRegistration(int userId)
        {
            return dbc.Registrations
                .Where(r => r.UserId == userId)
                .Include(r => r.Course)  // Eager load thông tin khóa học
                .ThenInclude(c => c.Teacher)
                .ToList();
        }

        // 7. Lấy đăng ký theo CourseId
        public List<Registration> GetByCourseIdRegistration(int courseId)
        {
            return dbc.Registrations
                .Where(r => r.CourseId == courseId)
                .Include(r => r.User)  // Eager load thông tin người dùng
                .ToList();
        }
        public void UpdateRegistrationStatus(int regisId, string newStatus)
        {
            var reg = dbc.Registrations.FirstOrDefault(r => r.RegistrationId == regisId);
            if (reg != null)
            {
                reg.Status = newStatus;
                dbc.SaveChanges(); 
            }
        }

        public void RejectRegistrationWithComment(int regisId, string comment)
        {
            var reg = dbc.Registrations.FirstOrDefault(r => r.RegistrationId == regisId);
            if (reg != null)
            {
                reg.Status = "Rejected";
                reg.Comments = comment;
                dbc.SaveChanges(); 
            }
        }

        //Lấy danh sách student theo courseID
        public List<StudentInCourse> GetStudentInCourses(int courseId)
        {
            var students = (from r in dbc.Registrations
                            join u in dbc.Users on r.UserId equals u.UserId
                            where r.CourseId == courseId && u.Role == "Student"
                            select new
                            {
                                u.UserId,
                                u.FullName,
                                u.Class,
                                u.School,
                                r.Status
                            }).ToList(); // Lấy về bằng LINQ to Entities trước

            // Sau đó xử lý AttendanceRate bằng LINQ to Objects
            var result = students.Select(s => new StudentInCourse
            {
                UserID = s.UserId,
                FullName = s.FullName,
                Class = s.Class,
                School = s.School,
                Status = s.Status,
                AttendanceRate = CalculateAttendanceRate(s.UserId, courseId)
            }).ToList();

            return result;
        }

        private double CalculateAttendanceRate(int userId, int courseId)
        {
            var all = dbc.Attendances.Count(a => a.UserId == userId && a.CourseId == courseId);
            if (all == 0) return 0;

            var present = dbc.Attendances.Count(a => a.UserId == userId && a.CourseId == courseId && a.Status == "Present");
            return Math.Round((present * 100.0) / all, 1);
        }
    }
}

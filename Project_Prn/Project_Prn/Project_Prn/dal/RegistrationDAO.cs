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
        public RegistrationDAO() { dbc = new Prngroup4Context(); }

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

    }
}

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
        public async Task<List<Registration>> GetAllRegistrationAsync()
        {
            return await dbc.Registrations
                .Include(r => r.User)  // Eager load thông tin người dùng
                .Include(r => r.Course) // Eager load thông tin khóa học
                .ToListAsync();
        }

        // 2. Lấy đăng ký theo ID
        public async Task<Registration?> GetByIdRegistrationAsync(int registrationId)
        {
            return await dbc.Registrations
                .Include(r => r.User)
                .Include(r => r.Course)
                .FirstOrDefaultAsync(r => r.RegistrationId == registrationId);
        }

        // 3. Thêm mới đăng ký
        public async Task AddRegistrationAsync(Registration registration)
        {
            dbc.Registrations.Add(registration);
            await dbc.SaveChangesAsync();
        }

        // 4. Cập nhật đăng ký
        public async Task UpdateRegistrationAsync(Registration registration)
        {
            dbc.Registrations.Update(registration);
            await dbc.SaveChangesAsync();
        }

        // 5. Xóa đăng ký
        public async Task DeleteRegistrationAsync(int registrationId)
        {
            var registration = await dbc.Registrations.FindAsync(registrationId);
            if (registration != null)
            {
                dbc.Registrations.Remove(registration);
                await dbc.SaveChangesAsync();
            }
        }

        // 6. Lấy đăng ký theo UserId
        public async Task<List<Registration>> GetByUserIdRegistrationAsync(int userId)
        {
            return await dbc.Registrations
                .Where(r => r.UserId == userId)
                .Include(r => r.Course)  // Eager load thông tin khóa học
                .ToListAsync();
        }

        // 7. Lấy đăng ký theo CourseId
        public async Task<List<Registration>> GetByCourseIdRegistrationAsync(int courseId)
        {
            return await dbc.Registrations
                .Where(r => r.CourseId == courseId)
                .Include(r => r.User)  // Eager load thông tin người dùng
                .ToListAsync();
        }

    }
}

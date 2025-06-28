using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Prn.dal
{
    public class UserDAO
    {
        private Prngroup4Context dbc;
        public UserDAO() { dbc = new Prngroup4Context(); }
    
    // 1. Lấy tất cả người dùng
        public async Task<List<User>> GetAllUserAsync()
        {
            return await dbc.Users
                .ToListAsync();
        }

        // 2. Lấy người dùng theo ID
        public async Task<User?> GetByIdUserAsync(int userId)
        {
            return await dbc.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        // 3. Thêm mới người dùng
        public async Task AddUserAsync(User user)
        {
            dbc.Users.Add(user);
            await dbc.SaveChangesAsync();
        }

        // 4. Cập nhật người dùng
        public async Task UpdateUserAsync(User user)
        {
            dbc.Users.Update(user);
            await dbc.SaveChangesAsync();
        }

        // 5. Xóa người dùng
        public async Task DeleteUserAsync(int userId)
        {
            var user = await dbc.Users.FindAsync(userId);
            if (user != null)
            {
                dbc.Users.Remove(user);
                await dbc.SaveChangesAsync();
            }
        }

        // 6. Lấy người dùng theo vai trò (Role)
        public async Task<List<User>> GetByRoleUserAsync(string role)
        {
            return await dbc.Users
                .Where(u => u.Role == role)
                .ToListAsync();
        }

        // 7. Tìm người dùng theo email
        public async Task<User?> GetByEmailUserAsync(string email)
        {
            return await dbc.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_Prn.dal
{
    public class UserDAO
    {
        private Prngroup4Context dbc;

        public UserDAO()
        {
            dbc = new Prngroup4Context();
        }

        // 1. Lấy tất cả người dùng
        public List<User> GetAllUser()
        {
            return dbc.Users.ToList(); 
        }

        // 2. Lấy người dùng theo ID
        public User GetByIdUser(int userId)
        {
            return dbc.Users.FirstOrDefault(u => u.UserId == userId); 
        }

        // 3. Thêm mới người dùng
        public void AddUser(User user)
        {
            dbc.Users.Add(user);
            dbc.SaveChanges(); 
        }

        // 4. Cập nhật người dùng
        public void UpdateUser(User user)
        {
            dbc.Users.Update(user);
            dbc.SaveChanges(); 
        }

        // 5. Xóa người dùng
        public void DeleteUser(int userId)
        {
            var user = dbc.Users.Find(userId); 
            if (user != null)
            {
                dbc.Users.Remove(user);
                dbc.SaveChanges(); 
            }
        }

        // 6. Lấy người dùng theo vai trò (Role)
        public List<User> GetByRoleUser(string role)
        {
            return dbc.Users.Where(u => u.Role == role).ToList();
        }

        // 7. Tìm người dùng theo email
        public User GetByEmailUser(string email)
        {
            return dbc.Users.FirstOrDefault(u => u.Email == email); 
        }
        // 8. Lấy người dùng theo FullName
        public List<User> GetByFullName(string name)
        {
            string keyword = name.Trim().ToLower();

            return dbc.Users
                      .Where(u => u.FullName.ToLower().Contains(keyword))
                      .ToList();
        }


        // 9. Xác thực người dùng khi đăng nhập
        public User ValidateLogin(string email, string password)
        {
            return dbc.Users.FirstOrDefault(u => u.Email == email && u.Password == password); 
        }

        public bool IsUserExist(int userId)
        {
            return dbc.Users.Any(u => u.UserId == userId);
        }
    }
}

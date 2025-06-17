using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Prn.dal
{
    public class AttendanceDAO
    {
        private Prngroup4Context dbc;
        public AttendanceDAO() { dbc = new Prngroup4Context(); }

        // lấy toàn bộ danh sách điểm danh
        public async Task<List<Attendance>> GetAttendancesAsync()
        {
            return await dbc.Attendances
                  .Include(a => a.User)
                  .Include(a => a.Course)
                  .ToListAsync();
        }
        // lay diem danh bang ID
        public async Task<Attendance> GetByIdAttancesAsync(int attendanceId)
        {
            return await dbc.Attendances
                .Include(a => a.User)
                .Include(a => a.Course)
                .FirstOrDefaultAsync(a => a.AttendanceId == attendanceId);
        }
        // them moi
        public async Task AddAsync(Attendance attendance)
        {         
                dbc.Attendances .Add(attendance);
                await dbc.SaveChangesAsync();
        }
        // cap nhat
        public async Task UpdateAsync(Attendance attendance)
        {
            dbc.Attendances.Update(attendance);
            await dbc.SaveChangesAsync();
        }
        // xoa
        public async Task DeleteAsync(int attendanceId)
        {
            var attendance = await dbc.Attendances.FindAsync(attendanceId);
            if (attendance != null)
            {
                dbc.Attendances.Remove(attendance);
                await dbc.SaveChangesAsync();
            }
        }
        // 6. lay danh sach diem danh theo User
        public async Task<List<Attendance>> GetByUserIdAsync(int userId)
        {
            return await dbc.Attendances
                .Where(a => a.UserId == userId)
                .Include(a => a.Course)
                .ToListAsync();
        }

        // 7. lay danh sach diem danh theo id
        public async Task<List<Attendance>> GetByCourseIdAsync(int courseId)
        {
            return await dbc.Attendances
                .Where(a => a.CourseId == courseId)
                .Include(a => a.User)
                .ToListAsync();
        }

    }

 }


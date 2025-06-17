using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Prn.dal
{
    public class ResultDAO
    {
        private Prngroup4Context dbc;
        public ResultDAO() { dbc = new Prngroup4Context(); }
    
     // 1. Lấy tất cả kết quả thi
        public async Task<List<Result>> GetAllResultAsync()
        {
            return await dbc.Results
                .Include(r => r.User)  // Eager load thông tin người dùng
                .Include(r => r.Exam)  // Eager load thông tin kỳ thi
                .ToListAsync();
        }

        // 2. Lấy kết quả thi theo ID
        public async Task<Result?> GetByIdResultAsync(int resultId)
        {
            return await dbc.Results
                .Include(r => r.User)
                .Include(r => r.Exam)
                .FirstOrDefaultAsync(r => r.ResultId == resultId);
        }

        // 3. Thêm mới kết quả thi
        public async Task AddResultAsync(Result result)
        {
            dbc.Results.Add(result);
            await dbc.SaveChangesAsync();
        }

        // 4. Cập nhật kết quả thi
        public async Task UpdateResultAsync(Result result)
        {
            dbc.Results.Update(result);
            await dbc.SaveChangesAsync();
        }

        // 5. Xóa kết quả thi
        public async Task DeleteResultAsync(int resultId)
        {
            var result = await dbc.Results.FindAsync(resultId);
            if (result != null)
            {
                dbc.Results.Remove(result);
                await dbc.SaveChangesAsync();
            }
        }

        // 6. Lấy kết quả thi theo ExamId
        public async Task<List<Result>> GetByExamIdResultAsync(int examId)
        {
            return await dbc.Results
                .Where(r => r.ExamId == examId)
                .Include(r => r.User)  // Eager load thông tin người dùng
                .ToListAsync();
        }

        // 7. Lấy kết quả thi theo UserId
        public async Task<List<Result>> GetByUserIdResultAsync(int userId)
        {
            return await dbc.Results
                .Where(r => r.UserId == userId)
                .Include(r => r.Exam)  // Eager load thông tin kỳ thi
                .ToListAsync();
        }
    }
}

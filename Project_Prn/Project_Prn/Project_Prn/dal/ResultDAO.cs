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

        public ResultDAO()
        {
            dbc = new Prngroup4Context();
        }

        // 1. Lấy tất cả kết quả thi
        public List<Result> GetAllResult()
        {
            return dbc.Results
                .Include(r => r.User)
                .Include(r => r.Exam)
                .ToList();
        }

        // 2. Lấy kết quả thi theo ID
        public Result? GetByIdResult(int resultId)
        {
            return dbc.Results
                .Include(r => r.User)
                .Include(r => r.Exam)
                .FirstOrDefault(r => r.ResultId == resultId);
        }

        // 3. Thêm kết quả thi
        public void AddResult(Result result)
        {
            dbc.Results.Add(result);
            dbc.SaveChanges();
        }

        // 4. Cập nhật kết quả thi
        public void UpdateResult(Result result)
        {
            dbc.Results.Update(result);
            dbc.SaveChanges();
        }

        // 5. Xóa kết quả thi
        public void DeleteResult(int resultId)
        {
            var result = dbc.Results.Find(resultId);
            if (result != null)
            {
                dbc.Results.Remove(result);
                dbc.SaveChanges();
            }
        }

        // 6. Lấy kết quả thi theo ExamId
        public List<Result> GetByExamIdResult(int examId)
        {
            return dbc.Results
                .Where(r => r.ExamId == examId)
                .Include(r => r.User)
                .ToList();
        }

        // 7. Lấy kết quả thi theo UserId
        public List<Result> GetByUserIdResult(int userId)
        {
            return dbc.Results
                .Where(r => r.UserId == userId)
                .Include(r => r.Exam)
                .ToList();
        }
    }
}

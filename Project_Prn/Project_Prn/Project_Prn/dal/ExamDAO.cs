using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Prn.dal
{
    public class ExamDAO
    {
        private Prngroup4Context dbc;
        public ExamDAO() { dbc = new Prngroup4Context(); }
    
     // 1. Lấy tất cả kỳ thi
        public async Task<List<Exam>> GetAllExamAsync()
        {
            return await dbc.Exams
                .Include(e => e.Course)  // Eager load thông tin khóa học
                .ToListAsync();
        }

        // 2. Lấy kỳ thi theo ID
        public async Task<Exam?> GetByIdExamAsync(int examId)
        {
            return await dbc.Exams
                .Include(e => e.Course)  // Eager load thông tin khóa học
                .FirstOrDefaultAsync(e => e.ExamId == examId);
        }

        // 3. Thêm mới kỳ thi
        public async Task AddExamAsync(Exam exam)
        {
            dbc.Exams.Add(exam);
            await dbc.SaveChangesAsync();
        }

        // 4. Cập nhật kỳ thi
        public async Task UpdateExamAsync(Exam exam)
        {
            dbc.Exams.Update(exam);
            await dbc.SaveChangesAsync();
        }

        // 5. Xóa kỳ thi
        public async Task DeleteExamAsync(int examId)
        {
            var exam = await dbc.Exams.FindAsync(examId);
            if (exam != null)
            {
                dbc.Exams.Remove(exam);
                await dbc.SaveChangesAsync();
            }
        }

        // 6. Lấy kỳ thi theo CourseId
        public async Task<List<Exam>> GetByCourseIdExamAsync(int courseId)
        {
            return await dbc.Exams
                .Where(e => e.CourseId == courseId)
                .Include(e => e.Course)  // Eager load thông tin khóa học
                .ToListAsync();
        }
    }
}

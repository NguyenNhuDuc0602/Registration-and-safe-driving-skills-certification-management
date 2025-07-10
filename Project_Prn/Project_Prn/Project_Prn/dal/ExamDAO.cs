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
        public ExamDAO()
        {
            dbc = new Prngroup4Context();
        }

        // 1. Lấy tất cả kỳ thi
        public List<Exam> GetAllExam()
        {
            return dbc.Exams
                .Include(e => e.Course)
                .ToList();
        }

        // 2. Lấy kỳ thi theo ID
        public Exam? GetByIdExam(int examId)
        {
            return dbc.Exams
                .Include(e => e.Course)
                .FirstOrDefault(e => e.ExamId == examId);
        }

        // 3. Thêm mới kỳ thi
        public void AddExam(Exam exam)
        {
            dbc.Exams.Add(exam);
            dbc.SaveChanges();
        }

        // 4. Cập nhật kỳ thi
        public void UpdateExam(Exam exam)
        {
            dbc.Exams.Update(exam);
            dbc.SaveChanges();
        }

        // 5. Xóa kỳ thi
        public void DeleteExam(int examId)
        {
            var exam = dbc.Exams.Find(examId);
            if (exam != null)
            {
                dbc.Exams.Remove(exam);
                dbc.SaveChanges();
            }
        }

        // 6. Lấy kỳ thi theo CourseId
        public List<Exam> GetByCourseIdExam(int courseId)
        {
            return dbc.Exams
                .Where(e => e.CourseId == courseId)
                .Include(e => e.Course)
                .ToList();
        }
    }
}

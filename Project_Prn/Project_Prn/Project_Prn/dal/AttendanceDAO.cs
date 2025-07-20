using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;

namespace Project_Prn.dal
{
    public class AttendanceDAO
    {
        private Prngroup4Context dbc;
        public AttendanceDAO(Prngroup4Context dbc)
        {
            dbc = new Prngroup4Context();
            this.dbc = dbc;
        }

        public List<AttendanceDetail> GetAttendanceDetails(int courseId, int userId)
        {
            return dbc.Attendances
                .Where(a => a.CourseId == courseId && a.UserId == userId)
                .OrderBy(a => a.SessionDate)
                .Select(a => new AttendanceDetail
                {
                    SessionDate = a.SessionDate,
                    Status = a.Status,
                    Note = a.Note
                }).ToList();
        }

        public void AddBulkAttendance(List<Attendance> attendances)
        {
            dbc.Attendances.AddRange(attendances);
            dbc.SaveChanges();
        }

        public int CountTotalSessions(int courseId)
        {
            return dbc.Attendances
                     .Where(a => a.CourseId == courseId)
                     .Select(a => a.SessionDate)
                     .Distinct()
                     .Count();
        }

        public int CountPresentSessions(int courseId, int userId)
        {
            return dbc.Attendances
                     .Count(a => a.CourseId == courseId && a.UserId == userId && a.Status == "Present");
        }

    }
}

namespace Project_Prn.Models
{
    public partial class Exam
    {
        public Exam()
        {
            Results = new HashSet<Result>();
            Questions = new HashSet<Question>();
        }

        public int ExamId { get; set; }
        public int CourseId { get; set; }
        public int? SupervisorId { get; set; }
        public DateOnly Date { get; set; }
        public string Room { get; set; }
        public DateTime ExamDate { get; set; }


        public bool IsConfirmed { get; set; }  

        public virtual Course Course { get; set; }
        public virtual User Supervisor { get; set; }

        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}

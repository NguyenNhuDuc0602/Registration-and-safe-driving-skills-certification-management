using System;
using System.Collections.Generic;

namespace Project_Prn.Models;

public partial class Exam
{
    public int ExamId { get; set; }

    public int CourseId { get; set; }

    public DateOnly Date { get; set; }

    public string Room { get; set; } = null!;

    public bool IsConfirmed { get; set; }

    public int? SupervisorId { get; set; }

    public DateTime ExamDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual User? Supervisor { get; set; }
}

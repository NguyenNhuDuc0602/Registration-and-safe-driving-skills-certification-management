using System;
using System.Collections.Generic;

namespace Project_Prn.Models;

public partial class ExamResult
{
    public int ResultId { get; set; }

    public int? UserId { get; set; }

    public int? ExamId { get; set; }

    public double? Score { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public virtual Exam? Exam { get; set; }

    public virtual User? User { get; set; }
}

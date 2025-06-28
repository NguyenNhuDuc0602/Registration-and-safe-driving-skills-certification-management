using System;
using System.Collections.Generic;

namespace Project_Prn.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int CourseId { get; set; }

    public int UserId { get; set; }

    public DateOnly SessionDate { get; set; }

    public string Status { get; set; } = null!;

    public string? Note { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

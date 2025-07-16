using System;
using System.Collections.Generic;

namespace Project_Prn.Models;

public partial class Registration
{
    public int RegistrationId { get; set; }

    public int UserId { get; set; }

    public int CourseId { get; set; }

    public string? Status { get; set; }

    public string? Comments { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual User User { get; set; } = null!;
}

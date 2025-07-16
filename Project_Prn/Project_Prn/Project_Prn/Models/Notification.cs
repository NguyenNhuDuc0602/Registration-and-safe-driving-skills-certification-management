using System;
using System.Collections.Generic;

namespace Project_Prn.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public int RegistrationId { get; set; }

    public virtual Registration Registration { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

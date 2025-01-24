using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class MissCalendar
{
    public int IdMissCalendar { get; set; }

    public int IdWorker { get; set; }

    public DateOnly MissDate { get; set; }

    public virtual Worker IdWorkerNavigation { get; set; } = null!;
}

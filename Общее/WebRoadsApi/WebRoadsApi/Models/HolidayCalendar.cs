using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class HolidayCalendar
{
    public int IdHolidayCalendar { get; set; }

    public int IdWorker { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Worker IdWorkerNavigation { get; set; } = null!;
}

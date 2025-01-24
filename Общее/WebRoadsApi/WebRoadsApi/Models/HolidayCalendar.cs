using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class HolidayCalendar
{
    public int IdHolidayCalendar { get; set; }

    public int IdWorker { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual Worker IdWorkerNavigation { get; set; } = null!;
}

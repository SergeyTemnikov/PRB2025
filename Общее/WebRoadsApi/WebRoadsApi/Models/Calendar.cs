using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class Calendar
{
    public int IdCalendar { get; set; }

    public int IdEvent { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public virtual Event IdEventNavigation { get; set; } = null!;
}

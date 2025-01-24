using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class EventStatus
{
    public int IdEventStatus { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}

using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class EventType
{
    public int IdEventType { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}

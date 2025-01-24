using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class Event
{
    public int IdEvent { get; set; }

    public string EventName { get; set; } = null!;

    public int IdEventType { get; set; }

    public int IdEventStatus { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Calendar> Calendars { get; set; } = new List<Calendar>();

    public virtual EventStatus IdEventStatusNavigation { get; set; } = null!;

    public virtual EventType IdEventTypeNavigation { get; set; } = null!;

    public virtual ICollection<TrainingCalendar> TrainingCalendars { get; set; } = new List<TrainingCalendar>();

    public virtual ICollection<TrainingMaterial> TrainingMaterials { get; set; } = new List<TrainingMaterial>();
}

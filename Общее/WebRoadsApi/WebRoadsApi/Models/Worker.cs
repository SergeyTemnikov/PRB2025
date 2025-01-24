using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class Worker
{
    public int IdWorker { get; set; }

    public string FullName { get; set; } = null!;

    public int IdPosition { get; set; }

    public string WorkPhoneNumber { get; set; } = null!;

    public int? IdCabinet { get; set; }

    public string Email { get; set; } = null!;

    public int? IdLead { get; set; }

    public int? IdHelper { get; set; }

    public int IdDepartament { get; set; }

    public int? IdPrivateInfo { get; set; }

    public bool IsWorking { get; set; }

    public DateOnly? LastWorkDay { get; set; }

    public virtual ICollection<Departament> Departaments { get; set; } = new List<Departament>();

    public virtual ICollection<HolidayCalendar> HolidayCalendars { get; set; } = new List<HolidayCalendar>();

    public virtual Cabinet? IdCabinetNavigation { get; set; }

    public virtual Worker? IdHelperNavigation { get; set; }

    public virtual Worker? IdLeadNavigation { get; set; }

    public virtual Position IdPositionNavigation { get; set; } = null!;

    public virtual WorkerPrivateInfo? IdPrivateInfoNavigation { get; set; }

    public virtual ICollection<Worker> InverseIdHelperNavigation { get; set; } = new List<Worker>();

    public virtual ICollection<Worker> InverseIdLeadNavigation { get; set; } = new List<Worker>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    public virtual ICollection<MissCalendar> MissCalendars { get; set; } = new List<MissCalendar>();

    public virtual ICollection<TrainingCalendar> TrainingCalendars { get; set; } = new List<TrainingCalendar>();
}

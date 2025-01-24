using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class TrainingCalendar
{
    public int IdTrainingCalendar { get; set; }

    public int IdTraining { get; set; }

    public int IdWorker { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public virtual Event IdTrainingNavigation { get; set; } = null!;

    public virtual Worker IdWorkerNavigation { get; set; } = null!;
}

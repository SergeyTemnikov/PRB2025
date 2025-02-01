using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class WorkerPrivateInfo
{
    public int IdInfo { get; set; }

    public int IdWorker { get; set; }

    public string? PrivatePhoneNumber { get; set; }

    public DateTime? Birthday { get; set; }

    public virtual Worker IdWorkerNavigation { get; set; } = null!;
}

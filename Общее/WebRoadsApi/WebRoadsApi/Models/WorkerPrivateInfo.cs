using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class WorkerPrivateInfo
{
    public int IdInfo { get; set; }

    public string? PrivatePhoneNumber { get; set; }

    public DateOnly? Birthday { get; set; }

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}

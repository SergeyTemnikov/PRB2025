using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class Cabinet
{
    public int IdCabinet { get; set; }

    public string CabinetNumber { get; set; } = null!;

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}

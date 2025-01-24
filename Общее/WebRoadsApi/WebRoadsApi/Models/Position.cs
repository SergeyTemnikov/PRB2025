using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class Position
{
    public int IdPosition { get; set; }

    public string NamePosition { get; set; } = null!;

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}

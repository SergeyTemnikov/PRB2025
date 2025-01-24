using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class MaterialStatus
{
    public int IdMaterialStatus { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}

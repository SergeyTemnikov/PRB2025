using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class MaterialType
{
    public int IdMaterialType { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}

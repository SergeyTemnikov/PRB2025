using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class Material
{
    public int IdMaterial { get; set; }

    public string MaterialName { get; set; } = null!;

    public DateOnly DataSucces { get; set; }

    public DateOnly? DateChanged { get; set; }

    public int IdStatus { get; set; }

    public int IdType { get; set; }

    public int IdAuthor { get; set; }

    public int? IdRegion { get; set; }

    public virtual Worker IdAuthorNavigation { get; set; } = null!;

    public virtual MaterialStatus IdStatusNavigation { get; set; } = null!;

    public virtual MaterialType IdTypeNavigation { get; set; } = null!;

    public virtual ICollection<TrainingMaterial> TrainingMaterials { get; set; } = new List<TrainingMaterial>();
}

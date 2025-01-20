using System;
using System.Collections.Generic;

namespace WebRoadsApi;

public partial class Departament
{
    public int IdDepartament { get; set; }

    public string NameDepartament { get; set; } = null!;

    public int? LevelDepartament { get; set; }

    public virtual ICollection<SubDepartament> SubDepartamentIdDaughterDepartamentNavigations { get; set; } = new List<SubDepartament>();

    public virtual ICollection<SubDepartament> SubDepartamentIdDepartamentNavigations { get; set; } = new List<SubDepartament>();
}

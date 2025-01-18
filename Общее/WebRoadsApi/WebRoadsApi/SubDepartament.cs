using System;
using System.Collections.Generic;

namespace WebRoadsApi;

public partial class SubDepartament
{
    public int IdSubDepartament { get; set; }

    public int IdDepartament { get; set; }

    public int IdDaughterDepartament { get; set; }

    public virtual Departament IdDaughterDepartamentNavigation { get; set; } = null!;

    public virtual Departament IdDepartamentNavigation { get; set; } = null!;
}

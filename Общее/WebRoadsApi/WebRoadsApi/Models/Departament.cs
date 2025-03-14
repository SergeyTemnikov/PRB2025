﻿using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class Departament
{
    public int IdDepartament { get; set; }

    public string NameDepartament { get; set; } = null!;

    public int? IdParentDepartament { get; set; }

    public string? DepartamentDescription { get; set; }

    public int? IdHeadOfDepartament { get; set; }

    public virtual Worker? IdHeadOfDepartamentNavigation { get; set; }

    public virtual Departament? IdParentDepartamentNavigation { get; set; }

    public virtual ICollection<Departament> InverseIdParentDepartamentNavigation { get; set; } = new List<Departament>();
}

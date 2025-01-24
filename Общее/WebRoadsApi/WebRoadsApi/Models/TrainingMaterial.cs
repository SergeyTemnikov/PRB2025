using System;
using System.Collections.Generic;

namespace WebRoadsApi.Models;

public partial class TrainingMaterial
{
    public int IdTrainingMaterial { get; set; }

    public int IdTraining { get; set; }

    public int IdMaterial { get; set; }

    public virtual Material IdMaterialNavigation { get; set; } = null!;

    public virtual Event IdTrainingNavigation { get; set; } = null!;
}

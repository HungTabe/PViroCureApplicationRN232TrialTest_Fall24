﻿using System;
using System.Collections.Generic;

namespace ViroCureDAL.Entities;

public partial class Virus
{
    public int VirusId { get; set; }

    public string VirusName { get; set; } = null!;

    public string? Treatment { get; set; }

    public virtual ICollection<PersonVirus> PersonViruses { get; set; } = new List<PersonVirus>();
}

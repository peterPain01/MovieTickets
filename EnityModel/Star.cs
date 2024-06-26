﻿using System;
using System.Collections.Generic;

namespace NetFlix.EnityModel;

public partial class Star
{
    public int StarId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}

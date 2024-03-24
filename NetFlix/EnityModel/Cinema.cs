using System;
using System.Collections.Generic;

namespace NetFlix.EnityModel;

public partial class Cinema
{
    public int CinemaId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}

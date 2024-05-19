﻿using System;
using System.Collections.Generic;

namespace NetFlix.EnityModel;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public DateTime? BirthDate { get; set; }

    public int? IsAdmin { get; set; }

    public string? Gender { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}

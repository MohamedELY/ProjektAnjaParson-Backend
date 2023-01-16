﻿using System;
using System.Collections.Generic;

namespace ProjektAnjaParson_Backend.Models;

public partial class Place
{
    public int Id { get; set; }

    public int? LocationId { get; set; }

    public string? PostId { get; set; }

    public string? Address { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Location? Location { get; set; }

    public virtual ICollection<Post> Posts { get; } = new List<Post>();
}
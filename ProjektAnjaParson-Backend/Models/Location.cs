using System;
using System.Collections.Generic;

namespace ProjektAnjaParson_Backend.Models;

public partial class Location
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Place> Places { get; } = new List<Place>();
}

using System;
using System.Collections.Generic;

namespace ProjektAnjaParson_Backend.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Icon { get; set; }

    public virtual ICollection<Place> Places { get; } = new List<Place>();
}

using System;
using System.Collections.Generic;

namespace ProjektAnjaParson_Backend.Models;

public partial class FirstName
{
    public int Id { get; set; }

    public string? FirstName1 { get; set; }

    public virtual ICollection<FullName> FullNames { get; } = new List<FullName>();
}

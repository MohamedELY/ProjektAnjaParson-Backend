using System;
using System.Collections.Generic;

namespace ProjektAnjaParson_Backend.Models;

public partial class User
{
    public int Id { get; set; }

    public int? FullNameId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual FullName? FullName { get; set; }

    public virtual ICollection<Post> Posts { get; } = new List<Post>();
}

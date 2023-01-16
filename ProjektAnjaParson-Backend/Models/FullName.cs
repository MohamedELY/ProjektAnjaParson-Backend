using System;
using System.Collections.Generic;

namespace ProjektAnjaParson_Backend.Models;

public partial class FullName
{
    public int Id { get; set; }

    public int? LastNameId { get; set; }

    public int? FirstNameId { get; set; }

    public virtual FirstName? FirstName { get; set; }

    public virtual LastName? LastName { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}

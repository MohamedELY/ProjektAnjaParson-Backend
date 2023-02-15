
namespace ProjektAnjaParson_Backend.Models;

public partial class Country
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Location> Locations { get; } = new List<Location>();
}

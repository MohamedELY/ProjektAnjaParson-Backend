
namespace ProjektAnjaParson_Backend.Models;

public partial class LastName
{
    public int Id { get; set; }

    public string? LastName1 { get; set; }

    public virtual ICollection<FullName> FullNames { get; } = new List<FullName>();
}


namespace ProjektAnjaParson_Backend.Models;

public partial class Post
{
    public int Id { get; set; }

    public int? PlaceId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? UserId { get; set; }

    public bool? Rating { get; set; }

    public virtual Place? Place { get; set; }

    public virtual User? User { get; set; }
}

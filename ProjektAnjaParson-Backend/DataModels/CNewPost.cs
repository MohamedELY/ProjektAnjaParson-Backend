
namespace ProjektAnjaParson_Backend.DataModels
{
    public class CNewPost
    {
        public int Id { get; set; }
        public int? PlaceId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }
        public bool? Rating { get; set; }
    }
}

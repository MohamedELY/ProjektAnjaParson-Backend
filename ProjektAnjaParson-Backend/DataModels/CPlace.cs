using ProjektAnjaParson_Backend.Models;

namespace ProjektAnjaParson_Backend.DataModels
{
    public class CPlace
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        public string? Address { get; set; }

        public string? Category { get; set; }

        public string? Pic { get; set; }
    }
}

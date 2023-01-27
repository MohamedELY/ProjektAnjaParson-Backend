using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.ApplicationDbContext;
using ProjektAnjaParson_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly AppDbContext _db;
        public IEnumerable<Location> Locations { get; set; }
        public Location Location { get; set; }

        public LocationController(AppDbContext db)
        {
            _db = db;
        }

        // GET api/<LocationController>
        [HttpGet]
        public IEnumerable<Location> Get()
        {
            Locations = _db.Locations;

            return Locations;
        }

        // GET api/<LocationController>/5
        [HttpGet("{id}")]
        public Location Get(int id)
        {
            Location = _db.Locations.Find(id);

            if(Location == null)
            {
                throw new NullReferenceException(
                @$"Object of type {typeof(Location)} could not be found. 
                Check if location with id {id} exists in database."
                );
            }
            return Location;
        }

        // POST api/<LocationController>
        [HttpPost]
        public void Post([FromBody] string name, int countryId)
        {
            using (var db = new AppDbContext())
            {
                var data = db.Locations;
                data.Add(new Location() { Name = name, CountryId = countryId });
                db.SaveChanges();
            }
        }

        // PUT api/<LocationController>/5
        [HttpPut("{id}")]
        public void Put(int id, string? name, int? countryId)
        {
            using (var db = new AppDbContext())
            {
                var data = db.Locations;

                var selected = data.SingleOrDefault(c => c.Id == id);
                if (selected != null)
                {
                    selected.Name= name ??= selected.Name;
                    selected.CountryId = countryId ??= selected.CountryId;
                    db.SaveChanges();
                }
            }
        }

        // DELETE api/<LocationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new AppDbContext())
            {
                var data = db.Locations;

                var selected = data.Single(c => c.Id == id);

                data.Remove(selected);
                db.SaveChanges();
            }
        }
    }
}

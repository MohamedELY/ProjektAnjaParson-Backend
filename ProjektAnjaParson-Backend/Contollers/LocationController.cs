using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.AppDbContext;
using ProjektAnjaParson_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        // GET: api/<LocationController>
        [HttpGet]
        public IEnumerable<Location> Get()
        {
            var data = new List<Location>();
            using (var db = new ApdatabaseContext())
            {
                data = db.Locations.ToList();
            }
            return data;
        }

        // GET api/<LocationController>/5
        [HttpGet("{id}")]
        public Location Get(int id)
        {
            var data = new Location();
            using (var db = new ApdatabaseContext())
            {
                data = db.Locations.SingleOrDefault(c => c.Id == id);
            }
            return data;
        }

        // POST api/<LocationController>
        [HttpPost]
        public void Post(string name, int countryId)
        {
            using (var db = new ApdatabaseContext())
            {
                var exist = db.Locations.SingleOrDefault(c => c.Name.ToLower() == name.ToLower());
                if (exist == null)
                {
                    var data = db.Locations;
                    data.Add(new Location() { Name = name, CountryId = countryId });
                    db.SaveChanges();
                }
            }
        }

        // PUT api/<LocationController>/5
        [HttpPut("{id}")]
        public void Put(int id, string? name, int? countryId)
        {
            using (var db = new ApdatabaseContext())
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
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var data = db.Locations;

                var selected = data.Single(c => c.Id == id);

                data.Remove(selected);
                db.SaveChanges();
            }
        }
    }
}

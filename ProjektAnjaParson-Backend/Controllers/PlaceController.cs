using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.Models;
using ProjektAnjaParson_Backend.ApplicationDbContext;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly AppDbContext _db;
        public IEnumerable<Place> Places { get; set; }
        public Place Place { get; set; }

        public PlaceController(AppDbContext db)
        {
            _db = db;
        }
        // GET: api/<PlaceController>
        [HttpGet]
        public IEnumerable<Place> Get()
        {
            var data = new List<Place>();
            using (var db = new AppDbContext())
            {
                data = db.Places.ToList();
            }
            return data;
        }

        // GET api/<PlaceController>/5
        [HttpGet("{id}")]
        public Place Get(int id)
        {
            var data = new Place();
            using (var db = new AppDbContext())
            {
                data = db.Places.SingleOrDefault(c => c.Id == id);
            }
            return data;
        }

        // POST api/<PlaceController>
        [HttpPost]
        public void Post(string name, int locationId, string adress, int categoryId)
        {
            using (var db = new AppDbContext())
            {
                var data = db.Places;
                data.Add(new Place() { Name = name, LocationId = locationId, Address = adress, CategoryId = categoryId });
                db.SaveChanges();
            }
        }

        // PUT api/<PlaceController>/5
        [HttpPut("{id}")]
        public void Put(int id, string? name, int? locationId, string? adress, int? categoryId)
        {
            using (var db = new AppDbContext())
            {
                var data = db.Places;

                var selected = data.SingleOrDefault(c => c.Id == id);
                if (selected != null)
                {
                    selected.Name = name ??= selected.Name;
                    selected.LocationId = locationId ??= selected.LocationId;
                    selected.Address = adress ??= selected.Address;
                    selected.CategoryId = categoryId ??= selected.CategoryId;
                    db.SaveChanges();
                }
            }
        }

        // DELETE api/<PlaceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new AppDbContext())
            {
                var data = db.Places;

                var selected = data.Single(c => c.Id == id);

                data.Remove(selected);
                db.SaveChanges();
            }
        }
    }
}

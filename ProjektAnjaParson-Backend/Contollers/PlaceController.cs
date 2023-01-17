using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        // GET: api/<PlaceController>
        [HttpGet]
        public IEnumerable<Place> Get()
        {
            var data = new List<Place>();
            using (var db = new AppDbContext.ApdatabaseContext())
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
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                data = db.Places.SingleOrDefault(c => c.Id == id);
            }
            return data;
        }

        // POST api/<PlaceController>
        [HttpPost]
        public void Post([FromBody] string name, int locationId, string adress, int categoryId)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var data = db.Places;
                data.Add(new Place() { Name = name, LocationId = locationId, Address = adress, CategoryId = categoryId });
                db.SaveChanges();
            }
        }

        // PUT api/<PlaceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var data = db.Places;

                var selected = data.SingleOrDefault(c => c.Id == id);
                if (selected != null)
                {
                    selected.Name = value;
                    db.SaveChanges();
                }
            }
        }

        // DELETE api/<PlaceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var data = db.Places;

                var selected = data.Single(c => c.Id == id);

                data.Remove(selected);
                db.SaveChanges();
            }
        }
    }
}

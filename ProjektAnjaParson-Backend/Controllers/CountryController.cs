using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.ApplicationDbContext;
using ProjektAnjaParson_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Crontollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContext _db;
        public IEnumerable<Country> Countries { get; set; }
        public Country Country { get; set; }

        public CountryController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/<CountryController>
        [HttpGet]
        public IEnumerable<Country> GetCountries()
        {
            Countries = _db.Countries;
            return Countries;
        }


        // GET api/<CountryController>/5
        [HttpGet("GetCountriesAsync/{id}")]
        public async Task<Country> GetCountriesAsync(int id)
        {
            Country = await _db.Countries.FindAsync(id);
            if(Country == null) { throw new NullReferenceException($"Object of type {typeof(Country)} cannot be null. Check if country with id {id} exists in database."); }
            return Country;
        }

        // POST api/<CountryController>
        [HttpPost]
        public void Post(string name)
        {
            var categoryCompare = _db.Categories.SingleOrDefault(c => c.Name == name);
            if (categoryCompare == null)
            {
                Country = new Country() { Name = name };
                _db.Countries.Add(Country);
                _db.SaveChanges();
            }
            else { throw new NullReferenceException($"Category {name} already exists in database."); }

            Console.WriteLine("Category Has been Saved to DB");
        }

        // PUT api/<CountryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string name)
        {
            using (var db = new AppDbContext())
            {
                var data = db.Countries;

                var selected = data.SingleOrDefault(c => c.Id == id);
                if (selected != null)
                {
                    selected.Name = name;
                    db.SaveChanges();
                }
            }
        }


        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}

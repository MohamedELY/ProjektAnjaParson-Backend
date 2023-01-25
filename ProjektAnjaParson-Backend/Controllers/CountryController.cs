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
            var countryCompare = _db.Countries.SingleOrDefault(c => c.Name == name);
            if (countryCompare == null)
            {
                _db.Countries.Add(new Country() { Name = name });
                _db.SaveChanges();
                Console.WriteLine("Category has been saved to Db");
            }
            else { throw new NullReferenceException($"Country {name} already exists in database."); }
        }

        // PUT api/<CountryController>/5
        [HttpPut("{id}")]
        public void Put(int id, string name)
        {
            Country = _db.Countries.Find(id);

            if (Country != null)
            {
                Country.Name = name;
                _db.Countries.Update(Country);
                _db.SaveChanges();
            }
        }


        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Country = _db.Countries.Find(id);
            if (Country != null)
            {
                _db.Countries.Remove(Country);
                _db.SaveChanges();
                Console.WriteLine("Country has been deleted from Db");
            }
        }
    }
}

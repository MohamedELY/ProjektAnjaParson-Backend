using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.AppDbContext;
using ProjektAnjaParson_Backend.DataModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchLocationController : ControllerBase
    {
        // GET: api/<SearchLocationController>
        [HttpGet]
        public IEnumerable<SearchLocation> Get()
        {
            using (var db = new ApdatabaseContext())
            {
                // Combines 4 tables to display Country, Location, Place, and Category Names into one list
                var query = (from c in db.Countries
                             join l in db.Locations on c.Id equals l.CountryId
                             join p in db.Places on l.Id equals p.LocationId
                             join cat in db.Categories on p.CategoryId equals cat.Id
                             select new SearchLocation
                             {
                                 Id = p.Id,
                                 Country = c.Name,
                                 Location = l.Name,
                                 Place = p.Name,
                                 Category = cat.Name,
                             }).ToList();
                return query;
            }
        }

        // GET api/<SearchLocationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/<SearchLocationController>/findLocation
        [HttpGet("{searchInput}/findLocation")]
        public IEnumerable<SearchLocation> Get(string? searchInput)
        {
            using (var db = new ApdatabaseContext())
            {
                // Combine tables into list
                var query = (from c in db.Countries
                             join l in db.Locations on c.Id equals l.CountryId
                             join p in db.Places on l.Id equals p.LocationId
                             join cat in db.Categories on p.CategoryId equals cat.Id
                             select new SearchLocation
                             {
                                 Id = p.Id,
                                 Country = c.Name,
                                 Location = l.Name,
                                 Place = p.Name,
                                 Category = cat.Name
                             }).ToList();


                List<SearchLocation> returnList = new List<SearchLocation>();

                // If searchbar is NOT null or whitespace, show items that contain the searched value
                if (!String.IsNullOrWhiteSpace(searchInput))
                {
                    // Converts the search string and list values to lowercase
                    // Searches through for items that contain the searched value

                    var findLocation = query.Where(c => c.Country.ToLower().Contains(searchInput.ToLower()) ||
                    c.Location.ToLower().Contains(searchInput.ToLower()) ||
                    c.Place.ToLower().Contains(searchInput.ToLower()) ||
                    c.Category.ToLower().Contains(searchInput.ToLower())).ToList();

                    returnList = new List<SearchLocation>(findLocation);
                    return returnList;
                }
                // Else return list with all items
                else return query;
            }
        }

        // POST api/<SearchLocationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SearchLocationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SearchLocationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

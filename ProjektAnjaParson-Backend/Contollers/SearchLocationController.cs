using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using ProjektAnjaParson_Backend.AppDbContext;
using ProjektAnjaParson_Backend.DataModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchLocationController : ControllerBase
    {
        private readonly ApdatabaseContext _db;
        private readonly ILogger _logger;
        public SearchLocationController(ApdatabaseContext db, ILogger<PostController> logger)
        {
            _db = db;
        }
        // GET: api/<SearchLocationController>
        [HttpGet]
        public IEnumerable<SearchLocation> Get()
        {

            // Combines 4 tables to display Country, Location, Place, and Category Names into one list
            var query =
                                        (from c in _db.Countries
                                         join l in _db.Locations on c.Id equals l.CountryId
                                         join p in _db.Places on l.Id equals p.LocationId
                                         join cat in _db.Categories on p.CategoryId equals cat.Id
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

        /*// GET api/<SearchLocationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // GET api/<SearchLocationController>/findLocation
        [HttpGet("{searchInput}/findLocation")]
        public IEnumerable<SearchLocation> Get(string? searchInput)
        {
            // Combine tables into list
            var query = (from c in _db.Countries
                         join l in _db.Locations on c.Id equals l.CountryId
                         join p in _db.Places on l.Id equals p.LocationId
                         join cat in _db.Categories on p.CategoryId equals cat.Id
                         select new SearchLocation
                         {
                             Id = p.Id,
                             Country = c.Name,
                             Location = l.Name,
                             Place = p.Name,
                             Category = cat.Name
                         }).ToList();


            // If searchbar is NOT null or whitespace, show items that contain the searched value
            if (!string.IsNullOrWhiteSpace(searchInput))
            {
                // Converts the search string and list values to lowercase
                // Searches through for items that contain the searched value

                var findLocation = query.Where(c => c.Country.ToLower().Contains(searchInput.ToLower()) ||
                c.Location.ToLower().Contains(searchInput.ToLower()) ||
                c.Place.ToLower().Contains(searchInput.ToLower()) ||
                c.Category.ToLower().Contains(searchInput.ToLower()))
                .ToList();

                var returnList = new List<SearchLocation>(findLocation);
                return returnList;
            }
            // Else return list with all items
            else
            {
                return query;
            }
        }

        // GET api/<SearchLocationController>/findLocation
        // [Route("")]
        [HttpGet("rating")]
        public List<CSearch> GetRating()
        {
            // Combine tables into list
            var query = (from c in _db.Countries
                                     join l in _db.Locations on c.Id equals l.CountryId
                                     join p in _db.Places on l.Id equals p.LocationId
                                     join cat in _db.Categories on p.CategoryId equals cat.Id
                                     join po in _db.Posts on p.Id equals po.PlaceId
                                     select new CSearch
                                     {
                                         Id = p.Id,
                                         Name = p.Name,
                                         Rating = po.Rating

                                     }).ToList();

            var dislike = query.Where(x => x.Id.Equals(1)).Where(x => x.Rating.Equals(false)).Count();
            var like = query.Where(x => x.Id.Equals(1)).Where(x => x.Rating.Equals(true)).Count();

            foreach(var item in query)
            {
                item.Dislike = query.Where(x => x.Id.Equals(item.Id)).Where(x => x.Rating.Equals(false)).Count();
                item.Like = query.Where(x => x.Id.Equals(item.Id)).Where(x => x.Rating.Equals(true)).Count();
            }

            var newList = query.Distinct().ToList();


            Console.WriteLine(dislike);


            // If searchbar is NOT null or whitespace, show items that contain the searched value
            /*if (!string.IsNullOrWhiteSpace(searchInput))
            {
                // Converts the search string and list values to lowercase
                // Searches through for items that contain the searched value

                var findLocation = query.Where(c => c.Country.ToLower().Contains(searchInput.ToLower()) ||
                c.Location.ToLower().Contains(searchInput.ToLower()) ||
                c.Place.ToLower().Contains(searchInput.ToLower()) ||
                c.Category.ToLower().Contains(searchInput.ToLower()))
                .ToList();

                var returnList = new List<SearchLocation>(findLocation);
                return returnList;
            }
            // Else return list with all items
            else
            {
                
            }*/

            return newList;
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
    }
}

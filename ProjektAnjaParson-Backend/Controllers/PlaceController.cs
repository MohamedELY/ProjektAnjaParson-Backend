
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public class PlaceController : ControllerBase
    {
        private readonly ApdatabaseContext _db;
        private readonly ILogger _logger;
        public PlaceController(ApdatabaseContext db, ILogger<PlaceController> logger)
        {
            _db = db;
            _logger = logger;
        }
        // GET: api/<PlaceController>
        [HttpGet]
        public ActionResult<IEnumerable<CPlace>> Get()
        {
            
            var query = (from p in _db.Places
                            join l in _db.Locations on p.LocationId equals l.Id
                            join c in _db.Countries on l.CountryId equals c.Id
                            join cat in _db.Categories on p.CategoryId equals cat.Id
                            select new CPlace
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Location = l.Name,
                                Address = p.Address,
                                Category = cat.Name,
                                Country = c.Name,
                                Pic = p.Pic
                            }).ToList();

            if(query == null)
            {
                _logger.Log(LogLevel.Error, "Places could not be retrieved from DB.");
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Places are being retrieved from DB.");
            return Ok(query);
        }

        // GET api/<PlaceController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<CPlace> Get(int id)
        {
            if (id < 1)
            {
                _logger.Log(LogLevel.Error, "Invalid ID for place. Must be a positive integer.");
                return BadRequest();
            }

            var exists = _db.Places.Find(id);
            if (exists == null)
            {
                _logger.Log(LogLevel.Error, "Could not find a place with id {id}", id);
                return NotFound();
            }
            var query = (from p in _db.Places
                            join l in _db.Locations on p.LocationId equals l.Id
                            join c in _db.Countries on l.CountryId equals c.Id
                            join cat in _db.Categories on p.CategoryId equals cat.Id
                            where p.Id == id
                            select new CPlace
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Location = l.Name,
                                Address = p.Address,
                                Category = cat.Name,
                                Country = c.Name,
                                Pic = p.Pic
                            }).First();

            if (query == null)
            {
                _logger.Log(LogLevel.Error, "Places could not be retrieved from DB.");
                return NotFound();
            }

            _logger.Log(LogLevel.Error, "Places are being retrieved from DB.");
            return Ok(query);
        }
    

        // POST api/<PlaceController>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult Post([FromBody] CPlace? place)
        {

            if(place == null)
            {
                _logger.Log(LogLevel.Error, "Invalid argument place name '{place}', check syntax.", place);
                return BadRequest();
            }

            var locationID = CPlace.CreateLocation(place.Location, place.Country);
            var categoryID = CPlace.GetCategoryID(place.Category);
            if (!HelperMethods.CheckIfIdsAreValid(locationID, categoryID))
            {
                _logger.Log(LogLevel.Error, "Invalid id, must be a positive integer.");
                return BadRequest();
            }

            _db.Places.Add(new Place()
            {
                Name = place.Name,
                LocationId = locationID,
                Address = place.Address,
                CategoryId = categoryID,
                Pic = place.Pic,
            });

            _logger.Log(LogLevel.Information, "Place {place.Name} added to the DB.", place.Name);
            _db.SaveChanges();

            return Ok();
        }

        // PUT api/<PlaceController>/5
        [HttpPut("{id}")]
        public void Put(int id, string? name, int? locationId, string? adress, int? categoryId)
        {

            var selected = _db.Places.Find(id);

            if (selected != null)
            {
                selected.Name = name ??= selected.Name;
                selected.LocationId = locationId ??= selected.LocationId;
                selected.Address = adress ??= selected.Address;
                selected.CategoryId = categoryId ??= selected.CategoryId;
                _db.SaveChanges();
            }
            
        }

        // DELETE api/<PlaceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var selected = _db.Places.Find(id);

            _db.Places.Remove(selected);

            _db.SaveChanges();
        }
    }
}

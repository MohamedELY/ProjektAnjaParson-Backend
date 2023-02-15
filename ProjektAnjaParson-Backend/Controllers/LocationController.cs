
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ApdatabaseContext _db;
        private readonly ILogger _logger;
        public LocationController(ApdatabaseContext db, ILogger<LocationController> logger)
        {
            _db = db;
            _logger = logger;
        }
        // GET: api/<LocationController>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<Location>> Get()
        {
            
            List<Location>? data = _db.Locations.ToList();
            if(data == null)
            {
                _logger.Log(LogLevel.Error, "Locations could not be retrieved from DB.");
                return NotFound();
            }
            _logger.Log(LogLevel.Information, "Locations are being retrieved from DB.");
            return Ok(data);
        }

        // GET api/<LocationController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<Location?> Get(int id)
        {
            if(id < 1)
            {
                _logger.Log(LogLevel.Error, "Invalid ID {id}, must be a positive integer.", id);
                return BadRequest();
            }

            Location? data = _db.Locations.Find(id);

            if(data == null)
            {
                _logger.Log(LogLevel.Error, "Location with ID {id} could not be retrieved from DB.", id);
                return NotFound();
            }
            _logger.Log(LogLevel.Information, "Location with ID {id} is being retrieved from DB.", id);
            return Ok(data);
        }

        // POST api/<LocationController>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult Post(string name)
        {
            if (!HelperMethods.CheckIfStringsAreValid(name))
            {
                _logger.Log(LogLevel.Error, "Invalid argument {name}, check syntax.", name);
                return BadRequest();
            }
            var exist = _db.Locations.SingleOrDefault(c => c.Name.ToLower() == name.ToLower());
            if (exist == null)
            {
                _db.Locations.Add(new Location() { Name = name });
                _db.SaveChanges();
                _logger.Log(LogLevel.Information, "Location with name {name} was added to the DB.", name);
                return Ok();
            }

            _logger.Log(LogLevel.Error, "Location {name} already exists in DB.", name);
            return StatusCode(StatusCodes.Status418ImATeapot);
        }

        // PUT api/<LocationController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult Put(int id, string? name, int? countryId)
        {
            var selected = _db.Locations.SingleOrDefault(c => c.Id == id);
            if (selected != null)
            {
                selected.Name= name ??= selected.Name;
                selected.CountryId = countryId ??= selected.CountryId;
                _db.SaveChanges();
                return Ok();
            }

            return NotFound();
        }

        // DELETE api/<LocationController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                _logger.Log(LogLevel.Error, "Invalid ID {id}, must be a positive integer.", id);
                return BadRequest();
            }

            var selected = _db.Locations.SingleOrDefault(c => c.Id == id);
            
            if(selected == null)
            {
                return NotFound();
            }

            _db.Locations.Remove(selected);
            _db.SaveChanges();
            return Ok();
        }
    }
}


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ApdatabaseContext _db;
        private readonly ILogger<CountryController> _logger;
        public CountryController(ApdatabaseContext db, ILogger<CountryController> logger)
        {
            _db = db;
            _logger = logger;
        }

        // GET: api/<CountryController>
        [HttpGet]
        public ActionResult<IEnumerable<Country>> Get()
        {
            var data = _db.Countries.ToList();
            if(data == null)
            {
                _logger.Log(LogLevel.Error, "Could not get countries from database.");
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Retrieving countries from DB");
            return Ok(data);
        }

        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public ActionResult<Country> Get(int id)
        {
            if (id < 1)
            {
                _logger.Log(LogLevel.Error, "Invalid ID, must be a positive integer, was {id}.", id);
                return BadRequest();
            }

            Country? data = _db.Countries.Find(id);

            if (data == null)
            {
                _logger.Log(LogLevel.Error, "Could not get country with id {id} from database.", id);
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Retrieving country {data.Name} from database.", data.Name);
            return Ok(data);
        }

        [HttpGet("api/cName")]
        public ActionResult<Country> Get(string cName)
        {
            if(cName == null)
            {
                _logger.Log(LogLevel.Error, "Invalid argument {cName}.", cName);
                return BadRequest();
            }
            var data = _db.Countries.SingleOrDefault(c => c.Name == cName);

            if (data == null)
            {
                _logger.Log(LogLevel.Error, "Could not get countries from database.");
                return NotFound();
            }
            _logger.Log(LogLevel.Information, "Retrieving country {cName} from database.", cName);
            return Ok(data);
        }

        // POST api/<CountryController>
        [HttpPost]
        public ActionResult Post([FromBody] string? name)
        {
            if (!HelperMethods.CheckIfStringsAreValid(name))
            {
                _logger.Log(LogLevel.Error, "Invalid argument '{name}'.", name);
                return BadRequest();
            }

            var exist = _db.Countries.SingleOrDefault(c => c.Name.ToLower() == name.ToLower());
            
            if (exist != null)
            {
                _logger.Log(LogLevel.Error, "Country {name} already exists in database.", name);
                return NotFound();
            }
            
            _db.Countries.Add(new Country() { Name = name });
            _logger.Log(LogLevel.Error, "Country {name} added to database.", name);
            _db.SaveChanges();
            return Ok();
        }

        // PUT api/<CountryController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string name)
        {
            var selected = _db.Countries.Find(id);
            if (selected != null)
            {
                selected.Name = name;
                _db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }


        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // Unused
        }
    }
}

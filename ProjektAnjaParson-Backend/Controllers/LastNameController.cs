
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LastNameController : ControllerBase
    {
        private readonly ApdatabaseContext _db;
        private readonly ILogger<LastNameController> _logger;
        public LastNameController(ApdatabaseContext db, ILogger<LastNameController> logger)
        {
            _db = db;
            _logger = logger;
        }
        // GET: api/<LastNameController>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<LastName>> Get()
        {
            var data = _db.LastNames.ToList();
            if (data == null)
            {
                _logger.Log(LogLevel.Error, "Could not retrieve last names from DB.");
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Retrieving last names from DB");
            return Ok(data);
        }

        // GET api/<LastNameController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<LastName> Get(int id)
        {
            if(id < 1)
            {
                _logger.Log(LogLevel.Error, "Invalid ID {id}, must be a positive integer.", id);
                return BadRequest();
            }
            var data = _db.LastNames.Find(id);

            if(data == null)
            {
                _logger.Log(LogLevel.Error, "Could not retrieve last name with ID {id} from DB.", id);
                return NotFound();
            }
            _logger.Log(LogLevel.Information, "Retrieving last name with ID {id} from DB.", id);
            return Ok(data);
        }

        [HttpGet("api/{lastName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<LastName> Get(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                _logger.Log(LogLevel.Error, "Invalid argument name '{lname}', must be a valid string.", lastName);
                return BadRequest();
            }

            var data = _db.LastNames.SingleOrDefault(c => c.LastName1 == lastName);
            if (data == null)
            {
                _logger.Log(LogLevel.Error, "Could not retrieve last name '{lname}' from DB.", lastName);
                return NotFound();
            }
            _logger.Log(LogLevel.Information, "Retrieving last name '{lname}' from DB.", lastName);
            return Ok(data);
        }

        // POST api/<LastNameController>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult Post([FromBody] string? lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                _logger.Log(LogLevel.Error, "Invalid argument name '{lname}', must be a valid string.", lastName);
                return BadRequest();
            }
            var exist = _db.LastNames.SingleOrDefault(c => c.LastName1.ToLower() == lastName.ToLower());
            if (exist == null)
            {
                var data = _db.LastNames;
                _db.LastNames.Add(new LastName() { LastName1 = lastName });
                _logger.Log(LogLevel.Information, "Last name '{lname}' added to DB.", lastName);
                _db.SaveChanges();
                return Ok();
            }

            _logger.Log(LogLevel.Warning, "Last name {lastname} already exists in DB.", lastName);
            return Problem("No new entry was added, already exists.");
        }

        // PUT api/<LastNameController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LastNameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

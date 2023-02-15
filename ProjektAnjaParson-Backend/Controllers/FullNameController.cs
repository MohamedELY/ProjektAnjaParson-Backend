
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FullNameController : ControllerBase
    {
        private readonly ApdatabaseContext _db;
        private readonly ILogger<FullNameController> _logger;
        public FullNameController(ApdatabaseContext db, ILogger<FullNameController> logger)
        {
            _db = db;
            _logger =logger;
        }
        //GET: api/<FullNameController>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<CFullName>> Get()
        {

            var query = (from flname in _db.FullNames
                                      join fname in _db.FirstNames on flname.FirstNameId equals fname.Id
                                      join lname in _db.LastNames on flname.LastNameId equals lname.Id
                                      select new CFullName
                                      {
                                          Id = flname.Id,
                                          FirstName = fname.FirstName1,
                                          LastName = lname.LastName1
                                      }).ToList();
            if(query == null)
            {
                _logger.Log(LogLevel.Error, "Could not retrieve full names from DB.");
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Retrieving full names from DB.");
            return Ok(query);
        }

        // GET api/<FullNameController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<CFullName> Get(int id)
        {
            if (!HelperMethods.CheckIfIdsAreValid(id))
            {
                _logger.Log(LogLevel.Error, "Invalid id {id}, must be a positive integer.", id);
                return BadRequest();
            }
            var query = (from flname in _db.FullNames
                                    join fname in _db.FirstNames on flname.FirstNameId equals fname.Id
                                    join lname in _db.LastNames on flname.LastNameId equals lname.Id
                                    where flname.Id == id
                                    select new CFullName
                                    {
                                        Id = flname.Id,
                                        FirstName = fname.FirstName1,
                                        LastName = lname.LastName1
                                    }).First();

            if(query == null)
            {
                _logger.Log(LogLevel.Error, "Could not retrieve full name with id {id} from DB.", id);
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Retrieving full name with id {id} from DB.", id);
            return Ok(query);
            
        }

        // POST api/<FullNameController>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult Post(int lnId, int fnId)
        {
            if(!HelperMethods.CheckIfIdsAreValid(lnId, fnId))
            {
                _logger.Log(LogLevel.Error, "Invalid id(s), must be positive integers - was {lnID} and {fnId}.", lnId, fnId);
                return BadRequest();
            }

            var exist = _db.FullNames.SingleOrDefault(c => c.FirstNameId == fnId && c.LastNameId == lnId);
            if (exist == null)
            {
                _db.FullNames.Add(new FullName() { FirstNameId = fnId, LastNameId = lnId });

                _db.SaveChanges();
                _logger.Log(LogLevel.Information, "Full name with first name id {fnId} and last name id {lndId} has been added.", fnId, lnId);
                return Ok();
            }

            _logger.Log(LogLevel.Warning, "Full name with first name id {fnId} and last name id {lnId} already exists.", fnId, lnId);
            return Problem("No new entry was added, already exists.");
        }

        // PUT api/<FullNameController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FullNameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

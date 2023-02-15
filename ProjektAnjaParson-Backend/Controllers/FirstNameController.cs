
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstNameController : ControllerBase
    {
        private readonly ApdatabaseContext _db;
        private readonly ILogger<FirstNameController> _logger;
        public FirstNameController(ApdatabaseContext db, ILogger<FirstNameController> logger)
        {
            _db = db;
            _logger = logger;
        }

        //GET: api/<FirstNameController>
        [HttpGet]
        public ActionResult<IEnumerable<FirstName>> Get()
        {
            var data = _db.FirstNames.ToList();
            if (data == null)
            {
                _logger.Log(LogLevel.Error, "Could not retrieve first names from DB.");
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Retrieving first names from DB");
            return Ok(data);
        }

        // GET api/<FirstNameController>/5
        [HttpGet("{id}")]
        public ActionResult<FirstName> Get(int id)
        {
            var data = _db.FirstNames.Find(id);

            if (data == null)
            {
                _logger.Log(LogLevel.Error, "Could not retrieve first name with ID {id} from DB.", id);
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Retrieving first name with ID {id} From DB", id);
            return Ok(data);
        }

        //GET api/<FirstNameController>/5
        [HttpGet("api/fname")]
        public ActionResult<FirstName> Get(string fname)
        {
            var data = _db.FirstNames.SingleOrDefault(c => c.FirstName1 == fname);

            if (data == null)
            {
                _logger.Log(LogLevel.Error, "Could not retrieve first name {fname} from DB.", fname);
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Retrieving first name with ID {fname} From DB", fname);
            return Ok(data);
        }

        // POST api/<FirstNameController>
        [HttpPost]
        public ActionResult Post(string firstName)
        {
            var exist = _db.FirstNames.SingleOrDefault(c => c.FirstName1.ToLower() == firstName.ToLower());
            if (exist == null)
            {
                _db.FirstNames.Add(new FirstName() { FirstName1 = firstName });
                _db.SaveChanges();
                _logger.Log(LogLevel.Information, "First name {firstName} has been saved to DB.", firstName);
                return Ok();
            }
            _logger.Log(LogLevel.Warning, "First name {firstName} already exists in db.", firstName);
            return StatusCode(StatusCodes.Status418ImATeapot);
        }

        // DELETE api/<FirstNameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var data = db.FirstNames.SingleOrDefault(c => c.Id == c.Id);
                if (data != null)
                {
                    db.FirstNames.Remove(data);

                    db.SaveChanges();
                }
            }
            Console.WriteLine("First Name Has been Deleted from DB");
        }
    }
}

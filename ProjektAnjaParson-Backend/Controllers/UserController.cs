
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApdatabaseContext _db;
        private readonly ILogger<UserController> _logger;
        public UserController(ApdatabaseContext db, ILogger<UserController> logger)
        {
            _db = db;
            _logger = logger;
        }
        // GET: api/<UserController>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<CUser>> Get()
        {
            
            var query = (from u in _db.Users
                            join flname in _db.FullNames on u.FullNameId equals flname.Id
                            join fname in _db.FirstNames on flname.FirstNameId equals fname.Id
                            join lname in _db.LastNames on flname.LastNameId equals lname.Id
                            select new CUser
                            {
                                Id = u.Id,
                                Firstname = fname.FirstName1,
                                Lastname = lname.LastName1,
                                Username = u.Username,
                                Password = u.Password
                            }).ToList();

            if(query == null)
            {
                _logger.Log(LogLevel.Error, "Could not retrieve users from DB.");
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Users are being retrieved from DB.");
            return Ok(query);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<CUser> Get(int id)
        {
            if(id < 1)
            {
                _logger.Log(LogLevel.Error, "Invalid user ID {id}, must be a positive integer.", id);
                return BadRequest();
            }

            var exists = _db.Users.Find(id);
            if(exists == null)
            {
                _logger.Log(LogLevel.Error, "User with id {id} does not exist in the database.", id);
                return NotFound();
            }

            var query = (from u in _db.Users
                                join flname in _db.FullNames on u.FullNameId equals flname.Id
                                join fname in _db.FirstNames on flname.FirstNameId equals fname.Id
                                join lname in _db.LastNames on flname.LastNameId equals lname.Id
                                where u.Id == id
                                select new CUser
                                {
                                    Id = u.Id,
                                    Firstname = fname.FirstName1,
                                    Lastname = lname.LastName1,
                                    Username = u.Username,
                                    Password = u.Password
                                }).First();

            if(query == null)
            {
                _logger.Log(LogLevel.Error, "Could not retrieve user with id {id}.", id);
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "User with id {id} is being retrieved from DB.", id);
            return Ok(query);
        }

        // POST api/<UserController>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Post([FromBody] CUser? user)
        {
            if (user == null)
            {
                _logger.Log(LogLevel.Error, $"Invalid argument, must be a {typeof(CUser)}.");
                return NotFound();
            }

            var checkExists = _db.Users.SingleOrDefault(u => u.Username == user.Username);

            if (checkExists != null)
            {
                _logger.Log(LogLevel.Warning, "User name {user.Username} already exists in database.", user.Username);
                return BadRequest();
            }

            var fullNameID = CFullName.CreateFullName(user.Firstname, user.Lastname);

            if (fullNameID < 1)
            {
                _logger.Log(LogLevel.Error, "Invalid full name ID '{id}', must be a positive integer.", fullNameID);
                return BadRequest();
            }

            _db.Users.Add(new User()
            {
                FullNameId = fullNameID,
                Username = user.Username,
                Password = Security.Hash.Execute(user.Password),
            });

            _db.SaveChanges();
            return Ok();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, int fullNameId, string password)
        {
            var selected = _db.Users.SingleOrDefault(c => c.Id == id);
            if (selected != null)
            {
                selected.FullNameId = fullNameId;
                selected.Password = password;
                _db.SaveChanges();
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Put(int id)
        {

            // Todo 

            //using (var db = new AppDbContext.ApdatabaseContext())
            //{
            //    var data = db.Users;
            //    var selected = data.SingleOrDefault(c => c.Id == id);

            //    if (selected != null)
            //    {
            //        //db.Remove(selected.Posts);
            //        db.Remove(selected);
            //        db.SaveChanges();
            //    }
            //}
        }
    }
}


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ApdatabaseContext _db;
        private readonly ILogger _logger;

        public LoginController(ApdatabaseContext db, ILogger<LoginController> logger)
        {
            _db = db;
            _logger = logger;
        }

        // GET api/<LoginController>/5
        [HttpGet("{username}/{password}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<CUser> Get(string userName, string password)
        {
            if(!HelperMethods.CheckIfStringsAreValid(userName, password))
            {
                _logger.Log(LogLevel.Error, "Invalid argument(s) {userName} and {password}, check syntax.", userName, password);
                return BadRequest();
            }

            string hPassword = Security.Hash.Execute(password);

            // users = caller.Get();

            List<CUser> users = (from u in _db.Users
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

            if(users == null)
            {
                return NotFound();
            }

            foreach (var user in users)
            {
                if(user.Username == userName && user.Password == hPassword )
                    return user;
            }
            return new CUser();
        }

        // GET api/<LoginController>/5
        [HttpGet("{username}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<CUser> GetUserByUsername(string username)
        {
            if (!HelperMethods.CheckIfStringsAreValid(username))
            {
                _logger.Log(LogLevel.Error, "Invalid argument {userName}, check syntax.", username);
                return BadRequest();
            }

            var data = _db.Users.SingleOrDefault(c => c.Username == username);

            if (data == null)
            {
                _logger.Log(LogLevel.Error, "Could not find user with username {userName} in DB.", username);
                return NotFound();
            }
            _logger.Log(LogLevel.Information, "User with username {userName} retrieved from DB.", username);
            return Ok(data);
        }
    }
}

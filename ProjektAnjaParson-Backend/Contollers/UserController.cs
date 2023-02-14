using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.Models;
using ProjektAnjaParson_Backend.DataModels;
using ProjektAnjaParson_Backend.AppDbContext;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
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
        public List<CUser> Get()
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var query = (from u in db.Users
                             join flname in db.FullNames on u.FullNameId equals flname.Id
                             join fname in db.FirstNames on flname.FirstNameId equals fname.Id
                             join lname in db.LastNames on flname.LastNameId equals lname.Id
                             select new CUser
                             {
                                 Id = u.Id,
                                 Firstname = fname.FirstName1,
                                 Lastname = lname.LastName1,
                                 Username = u.Username,
                                 Password = u.Password
                             }).ToList();
                return query;
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public CUser Get(int id)
        {
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
            return query;
        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult Post([FromBody] CUser user)
        {
            var checkExists = _db.Users.SingleOrDefault(u => u.Username == user.Username);

            if (checkExists != null)
            {
                _logger.Log(LogLevel.Warning, "User name {user.Username} already exists in database.", user.Username);
                return BadRequest();
            }

            var fullNameID = CFullName.CreateFullName(user.Firstname, user.Lastname);
            
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

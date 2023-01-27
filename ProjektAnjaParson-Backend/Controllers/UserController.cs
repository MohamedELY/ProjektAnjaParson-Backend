using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.Models;
using ProjektAnjaParson_Backend.DataModels;
using ProjektAnjaParson_Backend.ApplicationDbContext;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;
        public IEnumerable<User> Users { get; set; }
        public new User User { get; set; }

        public UserController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<CUser> Get()
        {
            IEnumerable<CUser> getUsers =
                (
                from u in _db.Users
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

            return getUsers;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public CUser Get(int id)
        {
            CUser getUser =
                (
                from u in _db.Users
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
            return getUser;

        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] int fullNameId, string username, string password)
        {
            var data = _db.Users;

            data.Add(new User()
            {
                FullNameId = fullNameId,
                Username = username,
                Password = password,
            });
            _db.SaveChanges();
       
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] int id, int fullNameId, string password)
        {
            
            var data = _db.Users;
            var selected = data.SingleOrDefault(c => c.Id == id);
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

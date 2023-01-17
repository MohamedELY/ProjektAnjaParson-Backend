using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var data = new List<User>();
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                data = db.Users.ToList();
            }
            return data;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            var data = new User();
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                data = db.Users.SingleOrDefault(c => c.Id == id);
            }
            return data;
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post(int fullNameId, string username, string password)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var data = db.Users;

                data.Add(new User()
                {
                    FullNameId = fullNameId,
                    Username = username,
                    Password = password,
                });
                db.SaveChanges();
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, int fullNameId, string password)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                
                var data = db.Users;

                var selected = data.SingleOrDefault(c => c.Id == id);
                if (selected != null)
                {
                    selected.FullNameId = fullNameId;
                    selected.Password = password;
                    db.SaveChanges();
                }
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

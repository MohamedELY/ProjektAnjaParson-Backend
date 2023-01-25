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
        // GET: api/<UserController>
        [HttpGet]
        public List<CUser> Get()
        {     
            using (var db = new AppDbContext())
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
            using (var db = new AppDbContext())
            {
                var query = (from u in db.Users
                            join flname in db.FullNames on u.FullNameId equals flname.Id
                            join fname in db.FirstNames on flname.FirstNameId equals fname.Id
                            join lname in db.LastNames on flname.LastNameId equals lname.Id
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

        }

        // POST api/<UserController>
        [HttpPost]
        public void Post(int fullNameId, string username, string password)
        {
            using (var db = new AppDbContext())
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
            using (var db = new AppDbContext())
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

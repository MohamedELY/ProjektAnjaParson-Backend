using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.ApplicationDbContext;
using ProjektAnjaParson_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstNameController : ControllerBase
    {
        private readonly AppDbContext _db;
        public IEnumerable<FirstName> FirstNames { get; set; }
        public FirstName FirstName { get; set; }

        public FirstNameController(AppDbContext db)
        {
            _db = db;
        }

        //GET: api/<FirstNameController>
        [HttpGet]
        public IEnumerable<FirstName> Get()
        {
            FirstNames = _db.FirstNames;

            if(FirstNames == null)
            {
                throw new NullReferenceException(
                $"Could not get first names from database. Check if server is running."
                );
            }

            Console.WriteLine("Retriving First Name's From DB");

            return FirstNames;
        }

        // GET api/<FirstNameController>/5
        [HttpGet("{id}")]
        public FirstName Get(int id)
        {
            FirstName = _db.FirstNames.Find(id);

            Console.WriteLine("Retriving First Name From DB");
            
            return FirstName;
        }

        //GET api/<FirstNameController>/5
        [HttpGet("{fname}")]
        public FirstName Get(string fname)
        {
            FirstName = _db.FirstNames.SingleOrDefault(f => f.FirstName1.ToLower() == fname.ToLower());

            Console.WriteLine("Retriving First Name From DB");
            
            return FirstName;
        }

        // POST api/<FirstNameController>
        [HttpPost]
        public void Post([FromBody] string firstName)
        {
            
            var exist = _db.FirstNames.SingleOrDefault(c => c.FirstName1.ToLower() == firstName.ToLower());
            if (exist == null)
            {
                var data = _db.FirstNames;
                data.Add(new FirstName() { FirstName1 = firstName });

                _db.SaveChanges();
                Console.WriteLine("First Name Has been Saved to DB");
            }
            else
            {
                throw new NullReferenceException("First name already exists. Check database.");
            }
        }

        // DELETE api/<FirstNameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new AppDbContext())
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

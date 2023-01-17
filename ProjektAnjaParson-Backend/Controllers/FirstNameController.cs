using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstNameController : ControllerBase
    {
        //GET: api/<FirstNameController>
        [HttpGet]
        public IEnumerable<FirstName> Get()
        {
            var data = new List<FirstName>();
            using (var db = new ApplicationDbContext.ApplicationDbContext())
            {
                data = db.FirstNames.ToList();
            }
            Console.WriteLine("Retriving First Name's From DB");
            return data;
        }

        // GET api/<FirstNameController>/5
        [HttpGet("{id}")]
        public FirstName Get(int id)
        {
            var data = new FirstName();
            using (var db = new ApplicationDbContext.ApplicationDbContext())
            {
                data = db.FirstNames.SingleOrDefault(c => c.Id == id); ;
            }
            Console.WriteLine("Retriving First Name From DB");
            return data;
        }

        // POST api/<FirstNameController>
        [HttpPost]
        public void Post(string firstName)
        {
            using (var db = new ApplicationDbContext.ApplicationDbContext())
            {
                var exist = db.FirstNames.SingleOrDefault(c => c.FirstName1.ToLower() == firstName.ToLower());
                if (exist == null)
                {
                    var data = db.FirstNames;
                    data.Add(new FirstName() { FirstName1 = firstName });
                    
                    db.SaveChanges();
                }
            }
            Console.WriteLine("First Name Has been Saved to DB");
        }

        // DELETE api/<FirstNameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new ApplicationDbContext.ApplicationDbContext())
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

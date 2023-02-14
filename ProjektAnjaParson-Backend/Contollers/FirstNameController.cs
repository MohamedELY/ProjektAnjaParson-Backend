using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.AppDbContext;
using ProjektAnjaParson_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstNameController : ControllerBase
    {
        private readonly ApdatabaseContext _db;
        public FirstNameController(ApdatabaseContext db)
        {
            _db = db;
        }

        //GET: api/<FirstNameController>
        [HttpGet]
        public ActionResult<IEnumerable<FirstName>> Get()
        {
            var data = _db.FirstNames.ToList();
            if (data == null)
            {
                return NotFound();
            }
            Console.WriteLine("Retriving First Name's From DB");
            return Ok(data);
        }

        // GET api/<FirstNameController>/5
        [HttpGet("{id}")]
        public ActionResult<FirstName> Get(int id)
        {
            var data = _db.FirstNames.SingleOrDefault(c => c.Id == id);

            if (data == null)
            {
                return NotFound();
            }

            Console.WriteLine("Retriving First Name From DB");

            return data;
        }

        //GET api/<FirstNameController>/5
        [HttpGet("{fname}")]
        public ActionResult<FirstName> Get(string fname)
        {
            var data = _db.FirstNames.SingleOrDefault(c => c.FirstName1 == fname);

            if (data == null)
            {
                return NotFound();
            }

            Console.WriteLine("Retriving First Name From DB");
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
                Console.WriteLine("First Name Has been Saved to DB");
                return Ok();
            }
            else
            {
                return Problem("First name {firstName} already exists in db.", firstName);
            }
            
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

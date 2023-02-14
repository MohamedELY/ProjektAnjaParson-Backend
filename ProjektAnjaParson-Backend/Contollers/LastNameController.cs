using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.AppDbContext;
using ProjektAnjaParson_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LastNameController : ControllerBase
    {
        private readonly ApdatabaseContext _db;
        public LastNameController(ApdatabaseContext db)
        {
            _db = db;
        }
        // GET: api/<LastNameController>
        [HttpGet]
        public ActionResult<IEnumerable<LastName>> Get()
        {
            
            var data = _db.LastNames.ToList();
            if (data == null)
            {
                return NotFound();
            }
            Console.WriteLine("Retriving Last Name's From DB");
            return Ok(data);
        }

        // GET api/<LastNameController>/5
        [HttpGet("{id}")]
        public ActionResult<LastName> Get(int id)
        {
            var data = _db.LastNames.Find(id);

            if(data == null)
            {
                return NotFound();
            }

            Console.WriteLine("Retriving Last Name From DB");

            return Ok(data);
        }

        [HttpGet("{lname}")]
        public LastName Get(string lname)
        {
            var data = new LastName();
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                data = db.LastNames.SingleOrDefault(c => c.LastName1 == lname);
            }
            Console.WriteLine("Retriving Last Name From DB");
            return data;
        }

        // POST api/<LastNameController>
        [HttpPost]
        public void Post([FromBody] string lastname)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var exist = db.LastNames.SingleOrDefault(c => c.LastName1.ToLower() == lastname.ToLower());
                if (exist == null)
                {
                    var data = db.LastNames;
                    data.Add(new LastName() { LastName1 = lastname });

                    db.SaveChanges();
                }
            }
            Console.WriteLine("Last Name Has been Saved to DB");
        }

        // PUT api/<LastNameController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LastNameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

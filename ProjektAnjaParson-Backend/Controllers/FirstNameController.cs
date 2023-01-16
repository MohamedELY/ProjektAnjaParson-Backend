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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FirstNameController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FirstNameController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            using (var db = new ApdatabaseContext())
            {
                var data = db.FirstNames;
                data.Add(new FirstName() { FirstName1 = value });
                db.SaveChanges();
            }
        }

        // PUT api/<FirstNameController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FirstNameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

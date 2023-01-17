using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FullNameController : ControllerBase
    {
        // GET: api/<FullNameController>
        [HttpGet]
        public IEnumerable<FullName> Get()
        {
            var data = new List<FullName>();
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                data = db.FullNames.ToList();
            }
            Console.WriteLine("Retriving Full Name's From DB");
            return data;
        }

        // GET api/<FullNameController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FullNameController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FullNameController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FullNameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

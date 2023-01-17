using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.Models;
using ProjektAnjaParson_Backend.DataModels;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FullNameController : ControllerBase
    {
        //GET: api/<FullNameController>
        [HttpGet]
        public List<CFullName> Get()
        {

            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var query = (from flname in db.FullNames
                             join fname in db.FirstNames on flname.FirstNameId equals fname.Id
                             join lname in db.LastNames on flname.LastNameId equals lname.Id
                             select new CFullName
                             {
                                 Id = flname.Id,
                                 FirstName = fname.FirstName1,
                                 LastName = lname.LastName1
                             }).ToList();
                Console.WriteLine("Retriving Full Name's From DB");
                return query;
            }
        }

        // GET api/<FullNameController>/5
        [HttpGet("{id}")]
        public CFullName Get(int id)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var query = (from flname in db.FullNames
                             join fname in db.FirstNames on flname.FirstNameId equals fname.Id
                             join lname in db.LastNames on flname.LastNameId equals lname.Id
                             where flname.Id == id
                             select new CFullName
                             {
                                 Id = flname.Id,
                                 FirstName = fname.FirstName1,
                                 LastName = lname.LastName1
                             }).First();
                Console.WriteLine("Retriving Full Name From DB");
                return query;
            }
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

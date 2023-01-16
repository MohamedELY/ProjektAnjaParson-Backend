using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        // GET: api/<CategoryController>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            var data = new List<Category>();
            using (var db = new ApdatabaseContext())
            {
                data = db.Categories.ToList();
            }
            return data;
        }

        //GET api/CategoryController/5
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            var data = new Category();
            using (var db = new ApdatabaseContext())
            {
                data = db.Categories.SingleOrDefault(c => c.Id == id);
            }
            return data;
        }

        // POST api/<CategoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            using (var db = new ApdatabaseContext())
            {
                var data = db.Categories;
                data.Add(new Category() { Name = value });
                db.SaveChanges();
            }

        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            using (var db = new ApdatabaseContext())
            {
                var data = db.Categories;

                var selected = data.SingleOrDefault(c => c.Id == id);
                if (selected != null) { 
                    db.SaveChanges();
                }
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
           
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.ApplicationDbContext;
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
            using (var db = new ApplicationDbContext.ApplicationDbContext())
            {
                data = db.Categories.ToList();
            }
            Console.WriteLine("Retriving Category's From DB");
            return data;
        }

        //GET api/CategoryController/5
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            var data = new Category();
            using (var db = new ApplicationDbContext.ApplicationDbContext())
            {
                data = db.Categories.SingleOrDefault(c => c.Id == id);
            }
            Console.WriteLine("Retriving Category From DB");
            return data;
        }

        // POST api/<CategoryController>
        [HttpPost]
        public void Post( string name, string icon)
        {
            using (var db = new ApplicationDbContext.ApplicationDbContext())
            {
                var data = db.Categories;
                data.Add(new Category() { Name = name, Icon = icon});
                db.SaveChanges();
            }
            Console.WriteLine("Category Has been Saved to DB");
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, string name, string icon)
        {
            using (var db = new ApplicationDbContext.ApplicationDbContext())
            {
                var data = db.Categories;

                var selected = data.SingleOrDefault(c => c.Id == id);
                if (selected != null) {
                    
                    selected.Name = name;
                    selected.Icon = icon;
                         
                    db.SaveChanges();
                }
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new ApdatabaseContext())
            {
                var data = db.Categories.SingleOrDefault(c => c.Id == c.Id);
                if (data != null)
                {
                    db.Categories.Remove(data);

                    db.SaveChanges();
                }
            }
            Console.WriteLine("Category Has been Deleted from DB");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.ApplicationDbContext;
using ProjektAnjaParson_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace ProjektAnjaParson_Backend.Crontollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _db;
        public IEnumerable<Category> Categories { get; set; }
        public Category Category { get; set; }

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            Categories = _db.Categories;
            if (Categories == null)
            {
                throw new NullReferenceException(
                $"Could not get categories from database. Check if server is running."
                );
            }
            return Categories;
        }

        //GET api/CategoryController/5
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            Category = _db.Categories.Find(id);
            if (Category == null)
            {
                throw new NullReferenceException(
                @$"Object of type {typeof(Category)} could not be found. 
                Check if category with id {id} exists in database."
                );
            }

            return Category;
        }
        // POST api/<CategoryController>
        [HttpPost]
        public void Post(string? name, string icon)
        {
            var categoryCompare = _db.Categories.SingleOrDefault(c => c.Name == name);
            if (categoryCompare == null)
            {
                Category = new Category() { Name = name, Icon = icon };
                _db.Categories.Add(Category);
                _db.SaveChanges();
                Console.WriteLine("Category Has been Saved to DB");
            }
            else
            {
                throw new NullReferenceException(
                @$"Could not add object of type {typeof(Category)}. 
                An object with the name {name} already exists in database."
                );
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, string name, string icon)
        {
            Category = _db.Categories.Find(id);
            var categoryCompare = _db.Categories.SingleOrDefault(c => c.Name == name);
            if (Category != null)
            {
                if (categoryCompare.Name != name)
                {
                    Category.Name = name;
                    Category.Icon = icon;

                    _db.Categories.Update(Category);
                    _db.SaveChanges();
                }
                else
                {
                    throw new NullReferenceException(
                        @$"Could not update object. {typeof(Category)} with name {name} already exists."
                    );
                }
            }
            else
            {
                throw new NullReferenceException(
                    @$"Could not update object. Object of type {typeof(Category)} could not be found. 
                    Check if category with id {id} exists in database."
                );
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Category = _db.Categories.Find(id);
            if (Category != null)
            {
                _db.Categories.Remove(Category);
                _db.SaveChanges();
                Console.WriteLine("Category Has been Deleted from DB");
            }
            else
            {
                throw new NullReferenceException(
                    @$"Could not delete object. Object of type {typeof(Category)} could not be found.
                    Check if category with id {id} exists in database."
                );
            }
        }
    }
}

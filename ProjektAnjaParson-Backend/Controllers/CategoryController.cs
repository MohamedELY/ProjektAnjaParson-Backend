using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApdatabaseContext _db;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ApdatabaseContext db, ILogger<CategoryController> logger)
        {
            _db = db;
            _logger = logger;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<Category>> Get()
        {
            
            var data = _db.Categories.ToList();

            if(data == null) 
            {
                _logger.Log(LogLevel.Error, "Could not retrieve categories from DB.");
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Retrieving categories From DB");

            return Ok(data);
        }

        //GET api/<CategoryController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<Category> Get(int id)
        {
            if (HelperMethods.CheckIfIdsAreValid(id) == false) 
            { 
                return BadRequest(); 
            }

            var data = _db.Categories.Find(id);

            if(data == null)
            {
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Retrieving category from DB");

            return Ok(data);
        }
        [Route("api/categoryname")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Category> Get(string cName)
        {
            var data = _db.Categories.SingleOrDefault(c => c.Name == cName);

            if(data == null)
            {
                _logger.Log(LogLevel.Error, "Could not find category {cName}", cName);
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Retrieving category {cName} from DB", cName);
            return Ok(data);
        }

        // POST api/<CategoryController>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult Post(string name, string? icon)
        {
            if(!HelperMethods.CheckIfStringsAreValid(name, icon))
            {
                _logger.Log(LogLevel.Error, "Parameter string cannot be empty or null");
                return BadRequest();
            }

            var newCategory = new Category();

            var existName = _db.Categories.SingleOrDefault(c => c.Name == name);
            if (existName != null)
            {
                _logger.Log(LogLevel.Warning, "Name {name} already exists in database.", name);
                newCategory.Name = existName.Name;
            }

            _logger.Log(LogLevel.Information, "Name {name} added to database.", name);
            newCategory.Name = name;

            var existIcon = _db.Categories.SingleOrDefault(c => c.Icon == icon);
            if (existIcon != null)
            {
                _logger.Log(LogLevel.Warning, "Icon URL {icon} already exists in database.", icon);
                newCategory.Icon = existIcon.Icon;
            }

            _logger.Log(LogLevel.Information, "Icon URL {icon} added to database.", icon);
            newCategory.Icon = icon;
            
            _db.Categories.Add(newCategory);
            _logger.Log(LogLevel.Information, "Category '{name}' added to the database.", name);
            _db.SaveChanges();

            return Ok();
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, string? name, string? icon)
        {
            Category? selected = _db.Categories.Find(id);
            if(selected == null)
            {
                _logger.Log(LogLevel.Error, "Could not find category with id {id}", id);
                return NotFound();
            }

            string? origName = selected.Name;
            if (selected != null)
            {
                selected.Name = name;
                selected.Icon = icon;

                _db.SaveChanges();
                _logger.Log(LogLevel.Information, "Category {origName} has been updated to {selected.Name}.", origName, selected.Name);
                
                return Ok();
            }

            _logger.Log(LogLevel.Error, "Could not find category {name}", name);

            return NotFound(name);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var data = _db.Categories.Find(id);
            if (data != null)
            {
                _db.Categories.Remove(data);
                _db.SaveChanges();
                Console.WriteLine("Category Has been Deleted from DB");

                return Ok();
            }

            return NotFound();
        }
    }
}

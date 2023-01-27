using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.Models;
using ProjektAnjaParson_Backend.ApplicationDbContext;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext _db;
        public IEnumerable<Post> Posts { get; set; }
        public Post PostFromDb { get; set; }
       
        public PostController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/PostController
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            var data = new List<Post>();
            using (var db = new AppDbContext())
            {
                data = db.Posts.ToList();
            }
            Console.WriteLine("Retriving Post's From DB");
            return data;
        }

        // GET api/PostController/5
        [HttpGet("{id}")]
        public Post Get(int id)
        {
            var data = new Post();
            using (var db = new AppDbContext())
            {
                data = db.Posts.SingleOrDefault(c => c.Id == id);
            }
            Console.WriteLine("Retriving Post From DB");
            return data;
        }

        // POST api/PostController
        [HttpPost]
        public void Post(int placeId, string title, string description, int userId, bool rating)
        {
            using (var db = new AppDbContext())
            {
                var data = db.Posts;
                data.Add(new Post() { PlaceId = placeId, Title = title, Description = description, UserId = userId, Rating = rating });
                db.SaveChanges();
            }
            Console.WriteLine("Post Has been Saved to DB");
        }

        // PUT api/PostController/5
        [HttpPut("{id}")]
        public void Put(int id, int? placeId, string? title, string? description, int? userId, bool? rating)
        {
            using (var db = new AppDbContext())
            {
                var data = db.Posts;

                var selected = data.SingleOrDefault(c => c.Id == id);
                if (selected != null)
                {

                    selected.PlaceId = placeId ??= selected.PlaceId;
                    selected.Title = title ??= selected.Title;
                    selected.Description = description ??= selected.Description;
                    selected.UserId = userId ??= selected.UserId;
                    selected.Rating = rating ??= selected.Rating;
                    db.SaveChanges();
                }
            }
        }

        // DELETE api/PostController/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new AppDbContext())
            {
                var data = db.Posts.SingleOrDefault(c => c.Id == c.Id);
                if (data != null)
                {
                    db.Posts.Remove(data);

                    db.SaveChanges();
                }
            }
            Console.WriteLine("Post Has been Deleted from DB");
        }
    }
}

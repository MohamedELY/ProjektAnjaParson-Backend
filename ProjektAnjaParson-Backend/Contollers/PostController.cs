using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.AppDbContext;
using ProjektAnjaParson_Backend.DataModels;
using ProjektAnjaParson_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApdatabaseContext _db;
        private readonly ILogger _logger;
        public PostController(ApdatabaseContext db, ILogger<PostController> logger)
        {
            _db = db;
        }
        // GET: api/PostController
        [HttpGet]
        public IEnumerable<CPost> Get()
        {
            
            var query = (from p in _db.Posts
                            join u in _db.Users on p.UserId equals u.Id
                            select new CPost
                            {
                                Id = p.Id,
                                PlaceId = p.PlaceId,
                                Title = p.Title,
                                Description = p.Description,
                                UserId = p.UserId,
                                Username = u.Username,
                                Rating = p.Rating
                            }).ToList();
            
            
            Console.WriteLine("Retriving Post's From DB");
            return query;
        }

        // GET api/PostController/5
        [HttpGet("{id}")]
        public IEnumerable<CPost> Get(int id)
        {
            
            var query = (from p in _db.Posts
                            join u in _db.Users on p.UserId equals u.Id
                            where id == p.PlaceId
                            select new CPost
                            {
                                Id = p.Id,
                                PlaceId = p.PlaceId,
                                Title = p.Title,
                                Description = p.Description,
                                UserId = p.UserId,
                                Username = u.Username,
                                Rating = p.Rating
                            }).ToList();

            Console.WriteLine("Retriving Post From DB");
            return query;
            
            
        }



        // POST api/PostController
        [HttpPost]
        public ActionResult Post([FromBody] CNewPost post)
        {
            if (post != null)
            {
                _db.Posts.Add(new Post()
                {
                    Id = post.Id,
                    PlaceId = post.PlaceId,
                    Title = post.Title,
                    Description = post.Description,
                    UserId = post.UserId,
                    Rating = post.Rating
                });
            }
            else
            {
                _logger.Log(LogLevel.Error, "Could not save post, must enter something.");
                return BadRequest();
            }

            _logger.Log(LogLevel.Information, "Post has been saved to DB.");
            _db.SaveChanges();
            return Ok();
        }

        // PUT api/PostController/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, int? placeId, string? title, string? description, int? userId, bool? rating)
        {

            var selected = _db.Posts.Find(id);
            if (selected != null)
            {
                selected.PlaceId = placeId ??= selected.PlaceId;
                selected.Title = title ??= selected.Title;
                selected.Description = description ??= selected.Description;
                selected.UserId = userId ??= selected.UserId;
                selected.Rating = rating ??= selected.Rating;
                _db.SaveChanges();

                return Ok();
            }

            return NotFound();
        }

        // DELETE api/PostController/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var data = _db.Posts.Find(id);
            if (data != null)
            {
                _db.Posts.Remove(data);
                _db.SaveChanges();
                Console.WriteLine("Post Has been Deleted from DB");

                return Ok(data);
            }

            return BadRequest();
        }
    }
}

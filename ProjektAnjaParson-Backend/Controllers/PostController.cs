
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
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
            _logger = logger;
        }
        // GET: api/PostController
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<CPost>> Get()
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

            if(query == null)
            {
                _logger.Log(LogLevel.Error, "Could not retrieve last names from DB.");
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Retriving posts from DB.");
            return Ok(query);
        }

        // GET api/PostController/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<CPost>> Get(int id)
        {
            if (id < 1)
            {
                _logger.Log(LogLevel.Error, "Invalid post ID '{id}', must be a positive integer.", id);
                return BadRequest();
            }
            var exists = _db.Places.Find(id);
            if(exists == null)
            {
                _logger.Log(LogLevel.Error, "Could not find post with id {id}.", id);
                return NotFound();
            }
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

            if(query == null)
            {
                _logger.Log(LogLevel.Error, "Could not find post with id {id}", id);
                return NotFound();
            }

            _logger.Log(LogLevel.Information, "Retriving posts from DB.");
            return Ok(query);
        }

        // POST api/PostController
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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

                _logger.Log(LogLevel.Information, "Post has been saved to DB.");
                return Ok();
            }
            
            _logger.Log(LogLevel.Error, "Could not save post, must enter something.");
            return BadRequest();
        }

        // PUT api/PostController/5
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
                _logger.Log(LogLevel.Information, "Post has been updated.");
                return Ok();
            }

            _logger.Log(LogLevel.Error, "Could not find post to update.");
            return NotFound();
        }

        // DELETE api/PostController/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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

            return NotFound();
        }
    }
}

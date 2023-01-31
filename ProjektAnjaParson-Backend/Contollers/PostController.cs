using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.DataModels;
using ProjektAnjaParson_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        // GET: api/PostController
        [HttpGet]
        public IEnumerable<CPost> Get()
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var query = (from p in db.Posts
                             join u in db.Users on p.UserId equals u.Id
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
                return query;
            }
            Console.WriteLine("Retriving Post's From DB");
        }

        // GET api/PostController/5
        [HttpGet("{id}")]
        public CPost Get(int id)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var query = (from p in db.Posts
                             join u in db.Users on p.UserId equals u.Id
                             where id == p.Id
                             select new CPost
                             {
                                 Id = p.Id,
                                 PlaceId = p.PlaceId,
                                 Title = p.Title,
                                 Description = p.Description,
                                 UserId = p.UserId,
                                 Username = u.Username,
                                 Rating = p.Rating
                             }).First();
                return query;
            }
            Console.WriteLine("Retriving Post From DB");
        }

        // POST api/PostController
        [HttpPost]
        public void Post(int placeId, string title, string description, int userId, bool rating)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
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
            using (var db = new AppDbContext.ApdatabaseContext())
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
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var data = db.Posts.SingleOrDefault(c => c.Id == id);
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

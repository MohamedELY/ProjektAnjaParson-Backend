using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.DataModels;
using ProjektAnjaParson_Backend.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        // GET: api/<PlaceController>
        [HttpGet]
        public IEnumerable<CPlace> Get()
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var query = (from p in db.Places
                             join l in db.Locations on p.LocationId equals l.Id
                             join c in db.Countries on l.CountryId equals c.Id
                             join cat in db.Categories on p.CategoryId equals cat.Id
                             select new CPlace
                             {
                                 Id = p.Id,
                                 Name = p.Name,
                                 Location = l.Name,
                                 Address = p.Address,
                                 Category = cat.Name,
                                 Pic = p.Pic
                             }).ToList();
                return query;
            }
        }

        // GET api/<PlaceController>/5
        [HttpGet("{id}")]
        public CPlace Get(int id)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var query = (from p in db.Places
                             join l in db.Locations on p.LocationId equals l.Id
                             join c in db.Countries on l.CountryId equals c.Id
                             join cat in db.Categories on p.CategoryId equals cat.Id
                             where p.Id == id
                             select new CPlace
                             {
                                 Id = p.Id,
                                 Name = p.Name,
                                 Location = l.Name,
                                 Address = p.Address,
                                 Category = cat.Name,
                                 Pic = p.Pic
                             }).First();
                return query;
            }
        }
    

        // POST api/<PlaceController>
        [HttpPost]
        public void Post([FromBody] CPlace place)
        {
            /*using (var db = new AppDbContext.ApdatabaseContext())
            {
                var data = db.Places;
                data.Add(new Place() {});
                db.SaveChanges();
            }*/

            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var data = db.Places;
                var locationID = CPlace.CreataLocation(place.Location, place.Country);
                var categoryID = CPlace.GetCategoryID(place.Category);
                

                data.Add(new Place()
                {
                    Name = place.Name,
                    LocationId = locationID,
                    Address = place.Address,
                    CategoryId = categoryID,
                    Pic = place.Pic,
                });
                db.SaveChanges();
            }
        }

        // PUT api/<PlaceController>/5
        [HttpPut("{id}")]
        public void Put(int id, string? name, int? locationId, string? adress, int? categoryId)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var data = db.Places;

                var selected = data.SingleOrDefault(c => c.Id == id);
                if (selected != null)
                {
                    selected.Name = name ??= selected.Name;
                    selected.LocationId = locationId ??= selected.LocationId;
                    selected.Address = adress ??= selected.Address;
                    selected.CategoryId = categoryId ??= selected.CategoryId;
                    db.SaveChanges();
                }
            }
        }

        // DELETE api/<PlaceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                var data = db.Places;

                var selected = data.Single(c => c.Id == id);

                data.Remove(selected);
                db.SaveChanges();
            }
        }
    }
}

﻿using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        // GET: api/<CountryController>
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            var data = new List<Country>();
            using (var db = new ApdatabaseContext())
            {
                data = db.Countries.ToList();
            }
            return data;
        }


        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public Country Get(int id)
        {
            var data = new Country();
            using (var db = new ApdatabaseContext())
            {
                data = db.Countries.SingleOrDefault(c => c.Id == id);
            }
            return data;
        }

        // POST api/<CountryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            using (var db = new ApdatabaseContext())
            {
                var data = db.Countries;
                data.Add(new Country() { Name = value });
                db.SaveChanges();
            }
        }

        // PUT api/<CountryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            using (var db = new ApdatabaseContext())
            {
                var data = db.Countries;

                var selected = data.SingleOrDefault(c => c.Id == id);
                if (selected != null)
                {
                    selected.Name = value;
                    db.SaveChanges();
                }
            }
        }


        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}

﻿using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.Models;
using ProjektAnjaParson_Backend.DataModels;
using System.Linq;
using ProjektAnjaParson_Backend.AppDbContext;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FullNameController : ControllerBase
    {
        private readonly ApdatabaseContext _db;
        public FullNameController(ApdatabaseContext db)
        {
            _db = db;
        }
        //GET: api/<FullNameController>
        [HttpGet]
        public ActionResult<List<CFullName>> Get()
        {

            var query = (from flname in _db.FullNames
                                        join fname in _db.FirstNames on flname.FirstNameId equals fname.Id
                                        join lname in _db.LastNames on flname.LastNameId equals lname.Id
                                        select new CFullName
                                        {
                                            Id = flname.Id,
                                            FirstName = fname.FirstName1,
                                            LastName = lname.LastName1
                                        }).ToList();
            if(query == null)
            {
                return NotFound();
            }

            Console.WriteLine("Retriving Full Name's From DB");

            return Ok(query);
            
        }

        // GET api/<FullNameController>/5
        [HttpGet("{id}")]
        public ActionResult<CFullName> Get(int id)
        {
            
            var query = (from flname in _db.FullNames
                                    join fname in _db.FirstNames on flname.FirstNameId equals fname.Id
                                    join lname in _db.LastNames on flname.LastNameId equals lname.Id
                                    where flname.Id == id
                                    select new CFullName
                                    {
                                        Id = flname.Id,
                                        FirstName = fname.FirstName1,
                                        LastName = lname.LastName1
                                    }).First();
            if(query == null)
            {
                return NotFound();
            }

            Console.WriteLine("Retriving Full Name From DB");
            return Ok(query);
            
        }

        // POST api/<FullNameController>
        [HttpPost]
        public ActionResult Post(int lnId, int fnId)
        {
            
            var exist = _db.FullNames.SingleOrDefault(c => c.FirstNameId == fnId && c.LastNameId == lnId);
            if (exist == null)
            {
                var data = _db.FullNames;
                data.Add(new FullName() { FirstNameId = fnId, LastNameId = lnId });

                _db.SaveChanges();
                Console.WriteLine("First Name Has been Saved to DB");
                return Ok();
            }
            return Problem($"Full name with first name id {fnId} and last name id {lnId} already exists.");
        }

        // PUT api/<FullNameController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FullNameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

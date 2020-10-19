using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieRentingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieRentingApiController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MovieRentingApiController));
       private IConfiguration _config;
        IRepository<MovieRent> _movierent;
        public MovieRentingApiController(IRepository<MovieRent> rentrepo,IConfiguration config)
        {
            _movierent = rentrepo;
            _config = config;
        }
        // GET: api/<MovieRentingApiController>
        [HttpGet]
        public IEnumerable<MovieRent> Get()
        {
            _log4net.Info("Data Arrived");
            return _movierent.GetAll();
         //  return NotFound();
        }

        // GET api/<MovieRentingApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MovieRentingApiController>
        [HttpPost]
        public IActionResult Post(MovieRent rent)
        {
            _log4net.Info("Inserting Started");
            using (var scope = new TransactionScope())
            {
                _movierent.Add(rent);
                scope.Complete();
                //return CreatedAtAction(nameof(Get), new { id = menuItem.Id }, menuItem);
                return Ok();
            }
        }

        // PUT api/<MovieRentingApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MovieRentingApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemperatureREST.Models;

namespace TemperatureREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        //Really we would use a DB, but for demo we'll use static
        public static List<Temperature> Data = new List<Temperature>();

        // GET: api/Temperature
        [HttpGet]
        //The return type can be just your type or Action<YourTye>, both will work, but latter also allows you to return error messages
        public IEnumerable<Temperature> Get()
        {
            return Data;
        }

        // GET: api/Temperature/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Temperature> Get(int id)
        {
            var result = Data.FirstOrDefault(x => x.Id == id);
            if(result == null)
            {
                return NotFound();// is resource doesn't exist, i'll return an eror
            }

            return result;
        }

        // POST: api/Temperature
        //for inserting a new resource
        [HttpPost]
        public ActionResult Post([FromBody] Temperature value)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            Data.Add(value);
            return Ok();
        }

        // PUT: api/Temperature/5
        //Replace an existing resource
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Temperature value)
        {
            var existing = Data.FirstOrDefault(x => x.Id == id);
            if (existing == null)
            {
                return NotFound();// is resource doesn't exist, i'll return an eror
            }

            Data.Remove(existing);
            value.Id = id;
            Data.Add(value);
            return Ok(); //Success = Ok()

        }

        // DELETE: api/ApiWithActions/5
        //Delete is for deleting resources
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existing = Data.FirstOrDefault(x => x.Id == id);
            if (existing == null)
            {
                return NotFound();// is resource doesn't exist, i'll return an eror
            }

            Data.Remove(existing);
            return Ok();
        }
    }
}

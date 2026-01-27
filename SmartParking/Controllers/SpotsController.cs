using Microsoft.AspNetCore.Mvc;
using SmartParking.Core.Services;
using SmartParking.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartParking.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotsController : ControllerBase
    {
        private readonly ISpotService _context;

        public SpotsController(ISpotService context)
        {
            _context = context;
        }
        // GET: api/<SpotsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SpotsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SpotsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SpotsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SpotsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace CocktailStrategist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkGraphController : ControllerBase
    {
        // GET: api/<DrinkGraphController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DrinkGraphController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DrinkGraphController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DrinkGraphController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DrinkGraphController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

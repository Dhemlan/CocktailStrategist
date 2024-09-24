using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CocktailStrategist.Controllers
{
    [Route("api/drink")]
    [ApiController]
    public class DrinkController : ControllerBase
    {
        // GET: api/<DrinkController>
        [HttpGet]
        public string Get()
        {
            return "value1";
        }

        // GET api/<DrinkController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DrinkController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DrinkController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DrinkController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

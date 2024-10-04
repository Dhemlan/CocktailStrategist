using CocktailStrategist.Data;
using CocktailStrategist.Services;
using CocktailStrategist.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CocktailStrategist.Controllers
{
    [Route("drink")]
    [ApiController]
    public class DrinkController: ControllerBase
    {
        private readonly IDrinkService _drinkService;

        public DrinkController(IDrinkService drinkService)
        {
            _drinkService = drinkService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drink>>> Get()
        {
            var content = await _drinkService.Get();
            return Ok(content);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value2";
        }

        [HttpPost]
        public void Post([FromBody] Drink drink)
        {
            _drinkService.Create(drink);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}

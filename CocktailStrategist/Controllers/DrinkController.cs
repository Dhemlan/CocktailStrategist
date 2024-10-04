using CocktailStrategist.Data;
using CocktailStrategist.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<Drink>> Get(Guid id)
        {
            var content = _drinkService.Get(id);
            return Ok(content);
        }

        [HttpPost]
        public void Post([FromBody] Drink drink)
        {
            _drinkService.Create(drink);
        }

        [HttpPut("{id}")]
        public void Put([FromBody] Drink drink)
        {
            _drinkService.Update(drink);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _drinkService.Delete(id);
        }
    }
}

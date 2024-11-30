using CocktailStrategist.Data;
using CocktailStrategist.Data.CreateRequestObjects;
using CocktailStrategist.Data.Enum;
using CocktailStrategist.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CocktailStrategist.Controllers
{
    [Route("ingredient")]
    [ApiController]
    public class IngredientController : ControllerBase
    {

        private IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Ingredient>>> Get()
        //{
        //    var content = await _ingredientService.Get();
        //    return Ok(content);
        //}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<IGrouping<IngredientCategory, Ingredient>>>> Get()
        {
            var content = await _ingredientService.GetAsCategories();
            return Ok(content);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Ingredient>> Get(Guid id)
        {
            var content = await _ingredientService.Get(id);
            return content == null ? NotFound() : content;
        }

        [HttpGet("map/{id}")]
        public async Task<IActionResult> GetLinkedIngredients(Guid id)
        {
            var content = await _ingredientService.GetWithDrinks(id);
            return Ok(content);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task Post([FromBody]CreateIngredientDTO ingredient)
        {
            await _ingredientService.Create(ingredient);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] Ingredient ingredient)
        {
            if (await _ingredientService.Get(ingredient.Id) == null)
            {
                return NotFound();
            }
            await _ingredientService.Update(ingredient);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var content = await _ingredientService.Delete(id);
            return content == null ? NotFound() : Ok();
        }
    }
}

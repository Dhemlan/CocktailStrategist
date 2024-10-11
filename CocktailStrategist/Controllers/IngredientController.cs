using CocktailStrategist.Data;
using CocktailStrategist.Services;
using CocktailStrategist.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public Task<ActionResult<IEnumerable<Ingredient>>> Get()
        {
            var content = new List<Ingredient>();
            return null;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Ingredient>> Get(Guid id)
        {
            var content = await _ingredientService.Get(id);
            return content == null ? NotFound() : content;
        }

        [HttpPost]
        public Task Post(Ingredient ingredient)
        {
            return null;//_ingredientService.Create(ingredient);
        }
    }
}

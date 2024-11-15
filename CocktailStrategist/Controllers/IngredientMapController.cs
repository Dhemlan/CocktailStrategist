using CocktailStrategist.Data;
using CocktailStrategist.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CocktailStrategist.Controllers
{
    [Route("map")]
    [ApiController]
    public class IngredientMapController: ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientMapController(IIngredientService ingredientMapService)
        {
            _ingredientService = ingredientMapService;
        }

        //[HttpGet("{id}")]
        //public ActionResult<Ingredient> Get(Guid id)
        //{

        //}
    }
}

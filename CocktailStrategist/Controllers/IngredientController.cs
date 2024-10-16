﻿using CocktailStrategist.Data;
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
        public async Task<ActionResult<IEnumerable<Ingredient>>> Get()
        {
            var content = await _ingredientService.Get();
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task Post(Ingredient ingredient)
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

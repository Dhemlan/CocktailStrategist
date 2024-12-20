﻿using AutoMapper;
using CocktailStrategist.Data;
using CocktailStrategist.Data.CreateRequestObjects;
using CocktailStrategist.Data.DTOs;
using CocktailStrategist.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CocktailStrategist.Controllers
{
    [Route("drink")]
    [ApiController]
    public class DrinkController: ControllerBase
    {
        private readonly IDrinkService _drinkService;
        private readonly IMapper _mapper;

        public DrinkController(IDrinkService drinkService, IMapper mapper)
        {
            _drinkService = drinkService;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Drink>>> Get()
        {
            var content = await _drinkService.Get();
            return Ok(content);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Drink>> Get(Guid id)
        {
            var content = await _drinkService.Get(id);
            return content == null ? NotFound() :  content;
        }

        [HttpGet("/drinkList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<DrinkDTOMini>>> GetDrinkList(Guid? ingredientId = null)
        {
            var content = await _drinkService.GetDrinkList(ingredientId);
            // TODO: no longer sufficient for notFound
            return content.Count() == 0 ? NotFound() : Ok(content);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateDrinkDTO drink)
        {
            try {
                await _drinkService.Create(drink);
            }
            // TODO: best practice for this type of exception
            catch (ArgumentException) {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] Drink drink)
        {
            if (await _drinkService.Get(drink.Id) == null)
            {
                return NotFound();
            }
            await _drinkService.Update(drink);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var content = await _drinkService.Delete(id);
            return content == null ? NotFound() : Ok();
        }
    }
}

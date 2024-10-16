﻿using CocktailStrategist.Data;
using CocktailStrategist.Repo.Interfaces;
using CocktailStrategist.Services.Interfaces;

namespace CocktailStrategist.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IBaseRepo<Drink> _repo;
        public DrinkService(IBaseRepo<Drink> repo) {
            _repo = repo;
        }
        public async Task Create(Drink drink)
        {
            _repo.Create(drink);
            await _repo.SaveAsync();
        }

        public Task<IEnumerable<Drink>> Get()
        {
            return _repo.GetAll();
        }

        public Task<Drink?> Get(Guid id)
        {
            return _repo.Get(id);
        }

        public async Task Update(Drink drink)
        {
            _repo.Update(drink);
            await _repo.SaveAsync();
        }

        public async Task<Drink?> Delete(Guid id)
        {
           var result = await _repo.Delete(id);
           await _repo.SaveAsync();
           return result;
        }
    }
}

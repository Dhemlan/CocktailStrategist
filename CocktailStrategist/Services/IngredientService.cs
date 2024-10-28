using CocktailStrategist.Data;
using CocktailStrategist.Data.Enum;
using CocktailStrategist.Repo.Interfaces;
using CocktailStrategist.Services.Interfaces;
using System.ComponentModel;

namespace CocktailStrategist.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepo _repo;
        public IngredientService(IIngredientRepo repo) {
            _repo = repo;
        }
        public async Task Create(Ingredient ingredient)
        {
            _repo.Create(ingredient);
            await _repo.SaveAsync();
        }

        public Task<IEnumerable<Ingredient>> Get()
        {
            return _repo.GetAll();
        }

        public Task<Ingredient?> Get(Guid id)
        {
            return _repo.Get(id);
        }

        public async Task<IEnumerable<IGrouping<IngredientCategory, Ingredient>>> GetAsCategories()
        {
            var ingredients = await _repo.GetAll();
            return ingredients.GroupBy(i => i.Category).ToList();
        }

        public async Task<IEnumerable<Ingredient>> GetMultiple(List<Guid> ingredientIds)
        {
            return await _repo.GetMultiple(ingredientIds);
        }

        public async Task Update(Ingredient ingredient)
        {
            _repo.Update(ingredient);
            await _repo.SaveAsync();
        }
        public async Task<Ingredient?> Delete(Guid id)
        {
            var result = await _repo.Delete(id);
            await _repo.SaveAsync();
            return result;
        }
    }
}

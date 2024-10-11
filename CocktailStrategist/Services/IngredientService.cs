using CocktailStrategist.Data;
using CocktailStrategist.Repo.Interfaces;
using CocktailStrategist.Services.Interfaces;

namespace CocktailStrategist.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IBaseRepo<Ingredient> _repo;
        public IngredientService(IBaseRepo<Ingredient> baseRepo) {
            _repo = baseRepo;
        }
        public async Task Create(Ingredient ingredient)
        {
            _repo.Create(ingredient);
            await _repo.SaveAsync();
        }

        public Task<IEnumerable<Ingredient>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Ingredient> Get(Guid id)
        {
            return _repo.Get(id);
        }

        public Task Update(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }
        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

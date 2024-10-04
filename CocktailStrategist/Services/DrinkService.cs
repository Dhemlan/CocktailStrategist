using CocktailStrategist.Data;
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
        public async void Create(Drink drink)
        {
            _repo.Create(drink);
            await _repo.SaveAsync();
        }

        public Task<IEnumerable<Drink>> Get()
        {
            return _repo.GetAll();
        }

        public Task<Drink> Get(Guid id)
        {
            return _repo.Get(id);
        }

        public async void Update(Drink drink)
        {
            _repo.Update(drink);
            await _repo.SaveAsync();
        }

        public async void Delete(Guid id)
        {
           await _repo.Delete(id);
           await _repo.SaveAsync();
        }
    }
}

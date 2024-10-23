using AutoMapper;
using CocktailStrategist.Data;
using CocktailStrategist.Data.CreateRequestObjects;
using CocktailStrategist.Repo.Interfaces;
using CocktailStrategist.Services.Interfaces;

namespace CocktailStrategist.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IBaseRepo<Drink> _repo;
        private readonly IMapper _mapper;

        private readonly IIngredientService _ingredientService;
        public DrinkService(IBaseRepo<Drink> repo, IMapper mapper, IIngredientService ingredientService) {
            _repo = repo;
            _mapper = mapper;
            _ingredientService = ingredientService;
        }
        public async Task Create(CreateDrinkRequest drinkRequest)
        {
            IEnumerable<Ingredient> ingredients = await _ingredientService.GetMultiple(drinkRequest.Ingredients);
            if (ingredients.Count() != drinkRequest.Ingredients.Count)
            {
                throw new ArgumentException("Non-existant ingredient found");
            }
           
            _repo.Create(new Drink { Id = Guid.Empty, Name = drinkRequest.Name, IngredientList = drinkRequest.Ingredients });
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

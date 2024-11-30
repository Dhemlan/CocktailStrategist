using AutoMapper;
using CocktailStrategist.Data;
using CocktailStrategist.Data.CreateRequestObjects;
using CocktailStrategist.Data.DTOs;
using CocktailStrategist.Repo.Interfaces;
using CocktailStrategist.Services.Interfaces;
using System.Security.AccessControl;

namespace CocktailStrategist.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkRepo _repo;
        private readonly IMapper _mapper;

        private readonly IIngredientService _ingredientService;
        private readonly IRecipeService _recipeService;
        public DrinkService(IDrinkRepo repo, IMapper mapper, IIngredientService ingredientService, IRecipeService recipeService) {
            _repo = repo;
            _mapper = mapper;
            _ingredientService = ingredientService;
            _recipeService = recipeService;
        }
        public async Task Create(CreateDrinkDTO drinkRequest)
        {
            IEnumerable<Ingredient> ingredients = await _ingredientService.GetMultiple(drinkRequest.Recipe.Ingredients.Select(i => i.IngredientId).ToList());
            if (ingredients.Count() != drinkRequest.Recipe.Ingredients.Count)
            {
                throw new ArgumentException("Non-existant ingredient found");
            }
            // Todo need a transaction here
            // _ingredientService.
            Drink newDrink = new Drink { Name = drinkRequest.Name };
            _repo.Create(newDrink);
            await _recipeService.Create(drinkRequest.Recipe, newDrink);

            //var drinks = await _repo.GetAll();
            //Drink newDrink = drinks.Where(drink => drink.Name.Equals(drinkRequest.Name)).First();
            await _ingredientService.UpdateForNewDrink(ingredients, newDrink);
            await _repo.SaveAsync();
        }

        public Task<IEnumerable<Drink>> Get()
        {
            return _repo.GetAll();
        }

        public async Task<Drink> Get(Guid id)
        {
            return await _repo.GetWithRecipes(id);
        }

        public async Task<IEnumerable<DrinkDTOMini>> GetDrinkList(Guid? ingredientId)
        {
            return  ingredientId == null ? await _repo.GetAllMini() : await _repo.GetMini((Guid)ingredientId);
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

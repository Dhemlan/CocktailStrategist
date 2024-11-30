using CocktailStrategist.Data;
using CocktailStrategist.Data.CreateRequestObjects;
using CocktailStrategist.Repo.Interfaces;
using CocktailStrategist.Services.Interfaces;

namespace CocktailStrategist.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IBaseRepo<Recipe> _repo;
        private readonly IIngredientUsageRepo _ingredientUsageRepo;
        public RecipeService(IBaseRepo<Recipe> repo, IIngredientUsageRepo ingredientUsageRepo)
        {
            _repo = repo;
            _ingredientUsageRepo = ingredientUsageRepo;
        }
        public async Task Create(CreateRecipeDTO recipe, Drink drink)
        {
            Recipe newRecipe = new Recipe { Drink = drink, Instructions = recipe.Instructions, Source = recipe.Source };
            // TODO: review this re: async lambda
            recipe.Ingredients.ForEach(async i =>  {
                var ingredientUsage = await
                    _ingredientUsageRepo.Get(
                        new IngredientUsage {IngredientId = i.IngredientId, Measurement = i.Measurement, Amount = i.Amount });
                newRecipe.IngredientUsages.Add(ingredientUsage);
            });
            
            _repo.Create(newRecipe);
            await _repo.SaveAsync();
        }
    }
}

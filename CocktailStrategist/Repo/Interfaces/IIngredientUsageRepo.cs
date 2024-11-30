using CocktailStrategist.Data;
using CocktailStrategist.Data.DTOs;

namespace CocktailStrategist.Repo.Interfaces
{
    public interface IIngredientUsageRepo
    {
        IngredientUsage Create(IngredientUsage ingredientUsage);

        Task<IngredientUsage> Get(IngredientUsage ingredientUsage);
    }
}

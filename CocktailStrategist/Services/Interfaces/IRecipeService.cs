using CocktailStrategist.Data;
using CocktailStrategist.Data.CreateRequestObjects;

namespace CocktailStrategist.Services.Interfaces
{
    public interface IRecipeService
    {
        Task Create(CreateRecipeDTO recipe, Drink drink);
    }
}

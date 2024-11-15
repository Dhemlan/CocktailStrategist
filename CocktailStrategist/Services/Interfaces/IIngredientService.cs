using CocktailStrategist.Data;
using CocktailStrategist.Data.CreateRequestObjects;
using CocktailStrategist.Data.Enum;

namespace CocktailStrategist.Services.Interfaces
{
    public interface IIngredientService
    {
        Task Create(CreateIngredientRequest ingredient);
        Task<IEnumerable<Ingredient>> Get();

        Task<IEnumerable<Ingredient>> GetMultiple(List<Guid> ingredientIds);

        Task<Ingredient?> Get(Guid id);
        Task<IEnumerable<Ingredient>> GetLinkedIngredients(Guid id);

        Task<IEnumerable<IGrouping<IngredientCategory, Ingredient>>> GetAsCategories();

        Task<Ingredient> GetWithDrinks(Guid id);

        Task Update(Ingredient ingredient);

        Task UpdateForNewDrink(IEnumerable<Ingredient> ingredients, Drink drink);

        Task<Ingredient?> Delete(Guid id);
    }
}

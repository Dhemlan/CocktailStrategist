using CocktailStrategist.Data;
using CocktailStrategist.Data.Enum;

namespace CocktailStrategist.Services.Interfaces
{
    public interface IIngredientService
    {
        Task Create(Ingredient ingredient);
        Task<IEnumerable<Ingredient>> Get();

        Task<IEnumerable<Ingredient>> GetMultiple(List<Guid> ingredientIds);

        Task<Ingredient?> Get(Guid id);

        Task<IEnumerable<IGrouping<IngredientCategory, Ingredient>>> GetAsCategories();

        Task Update(Ingredient ingredient);

        Task<Ingredient?> Delete(Guid id);
    }
}

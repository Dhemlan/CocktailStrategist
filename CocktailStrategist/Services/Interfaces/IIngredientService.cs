using CocktailStrategist.Data;

namespace CocktailStrategist.Services.Interfaces
{
    public interface IIngredientService
    {
        Task Create(Ingredient ingredient);
        Task<IEnumerable<Ingredient>> Get();

        Task<IEnumerable<Ingredient>> GetMultiple(List<Guid> ingredientIds);

        Task<Ingredient?> Get(Guid id);

        Task Update(Ingredient ingredient);

        Task<Ingredient?> Delete(Guid id);
    }
}

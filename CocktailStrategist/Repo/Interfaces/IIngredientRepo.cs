using CocktailStrategist.Data;

namespace CocktailStrategist.Repo.Interfaces
{
    public interface IIngredientRepo: IBaseRepo<Ingredient>
    {
        Task<List<Ingredient>> GetMultiple(List<Guid> ingredientIds);
        Task<Ingredient> GetWithDrinks(Guid id);
    }
}

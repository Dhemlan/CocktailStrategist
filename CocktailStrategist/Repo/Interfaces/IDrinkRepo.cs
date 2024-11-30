using CocktailStrategist.Data;
using CocktailStrategist.Data.DTOs;

namespace CocktailStrategist.Repo.Interfaces
{
    public interface IDrinkRepo: IBaseRepo<Drink>
    {
        Task<Drink> GetWithRecipes(Guid id);
        Task<IEnumerable<DrinkDTOMini>> GetAllMini();
        Task<IEnumerable<DrinkDTOMini>> GetMini(Guid ingredientId);

    }

}

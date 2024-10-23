using CocktailStrategist.Data;
using CocktailStrategist.Data.CreateRequestObjects;

namespace CocktailStrategist.Services.Interfaces
{
    public interface IDrinkService
    {
        Task Create(CreateDrinkRequest drink); 
        Task<IEnumerable<Drink>> Get();

        Task<Drink?> Get(Guid id);

        Task Update(Drink drink);

        Task<Drink?> Delete(Guid id);


    }
}

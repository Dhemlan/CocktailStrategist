using CocktailStrategist.Data;
using CocktailStrategist.Data.CreateRequestObjects;
using CocktailStrategist.Data.DTOs;

namespace CocktailStrategist.Services.Interfaces
{
    public interface IDrinkService
    {
        Task Create(CreateDrinkDTO drink); 
        Task<IEnumerable<Drink>> Get();

        Task<Drink?> Get(Guid id);

        Task<IEnumerable<DrinkDTOMini>> GetDrinkList(Guid? ingredientId);

        Task Update(Drink drink);

        Task<Drink?> Delete(Guid id);


    }
}

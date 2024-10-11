using CocktailStrategist.Data;

namespace CocktailStrategist.Services.Interfaces
{
    public interface IDrinkService
    {
        Task Create(Drink drink); 
        Task<IEnumerable<Drink>> Get();

        Task<Drink> Get(Guid id);

        Task Update(Drink drink);

        Task<Drink?> Delete(Guid id);


    }
}

using CocktailStrategist.Data;

namespace CocktailStrategist.Services.Interfaces
{
    public interface IDrinkService
    {
        void Create(Drink drink); 
        Task<IEnumerable<Drink>> Get();

        Task<Drink> Get(Guid id);

        void Update(Drink drink);

        void Delete(Guid id);


    }
}

using CocktailStrategist.Data;

namespace CocktailStrategist.Services.Interfaces
{
    public interface IDrinkService
    {
        IEnumerable<Drink> Get(string ingredient);

        void Create(Drink drink);


    }
}


namespace CocktailStrategist.Data
{
    public class Drink
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public List<IngredientUsage> IngredientList { get; } = new List<IngredientUsage>();
    }
}
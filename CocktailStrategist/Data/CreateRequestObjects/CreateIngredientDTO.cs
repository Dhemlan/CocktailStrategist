using CocktailStrategist.Data.Enum;

namespace CocktailStrategist.Data.CreateRequestObjects
{
    public class CreateIngredientDTO
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }

        public IngredientCategory Category { get; set; }
    }
}

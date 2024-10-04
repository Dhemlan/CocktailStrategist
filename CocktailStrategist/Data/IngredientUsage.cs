namespace CocktailStrategist.Data
{
    public class IngredientUsage
    {
        public Guid Id { get; set; }
        public required Ingredient Ingredient { get; set; }
        public required string Measurement { get; set; }

    }
}

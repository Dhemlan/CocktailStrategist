namespace CocktailStrategist.Data.DTOs
{
    public class IngredientUsageDTO
    {
        public Guid IngredientId { get; set; }

        public decimal Amount { get; set; }

        public string Measurement { get; set; }
    }
}
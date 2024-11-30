using Microsoft.EntityFrameworkCore;

namespace CocktailStrategist.Data
{
    [PrimaryKey(nameof(IngredientId), nameof(Measurement), nameof(Amount))]
    public class IngredientUsage
    {

        public List<Recipe> Recipes { get; set; }
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        [Precision(4, 2)]
        public decimal Amount { get; set; }
        public string Measurement { get; set; }

    }
}

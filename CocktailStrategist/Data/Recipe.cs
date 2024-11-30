using CocktailStrategist.Data.CreateRequestObjects;
using System.ComponentModel.DataAnnotations;

namespace CocktailStrategist.Data
{
    public class Recipe
    {
        public Guid Id{ get; set; }
        public Drink Drink { get; set; } = null!;
        public Guid DrinkId { get; set; }
        public string Instructions{ get; set; }

        public bool IsDefault { get; set; }

        [Range(1, 10)]
        public int Rating { get; set; }

        public string Source{ get; set; }

        public ICollection<IngredientUsage> IngredientUsages { get; set; } = new List<IngredientUsage>();

    }
}


using Microsoft.EntityFrameworkCore;

namespace CocktailStrategist.Data
{
    [Index(nameof(Name), IsUnique = true)]
    public class Drink
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
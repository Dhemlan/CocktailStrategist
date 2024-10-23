
using Microsoft.EntityFrameworkCore;

namespace CocktailStrategist.Data
{
    [Index(nameof(Name), IsUnique = true)]
    public class Drink
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public List<Guid> IngredientList { get; set; } = new List<Guid>();
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}

using Microsoft.EntityFrameworkCore;

namespace CocktailStrategist.Data
{
    [Index(nameof(Name), IsUnique = true)]
    public class Drink
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        //private IEnumerable<Ingredient>? _ingredients;
        //public IEnumerable<Ingredient> Ingredients => _ingredients ??= new List<Ingredient>();
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
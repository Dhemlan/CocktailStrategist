using CocktailStrategist.Data.Enum;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace CocktailStrategist.Data
{
    [Index(nameof(Name), IsUnique = true)]
    public class Ingredient
    {
        public Guid Id { get; set; }
        
        public required string Name { get; set; }

        public bool isAvailable { get; set; }

        public IngredientCategory Category { get; set; }

        public ICollection<Drink> Drinks { get; } = new List<Drink>();

        public ICollection<IngredientUsage> IngredientUsages { get; set; }   
    }
}


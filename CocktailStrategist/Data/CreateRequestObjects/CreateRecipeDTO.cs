using CocktailStrategist.Data.DTOs;

namespace CocktailStrategist.Data.CreateRequestObjects
{
    public class CreateRecipeDTO
    {
        public string Instructions { get; set; }

        public string Source { get; set; }
        public List<IngredientUsageDTO> Ingredients { get; set; }
    }
}

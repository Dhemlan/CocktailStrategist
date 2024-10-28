namespace CocktailStrategist.Data
{
    public class Recipe
    {
        public Guid Id{ get; set; }
        public string Instructions{ get; set; }

        public string Source{ get; set; }

        public List<IngredientUsage> Ingredients { get; set; } = new List<IngredientUsage>();

    }
}

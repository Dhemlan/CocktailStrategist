namespace CocktailStrategist.Data.DTOs
{
    public class DrinkDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Guid> Ingredients{ get; set; } = new List<Guid>();

        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}

namespace CocktailStrategist.Data
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public List<Drink> Drinks { get; } = new List<Drink>();
    }
}

namespace CocktailStrategist.Data
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Drink> Drinks { get; } = new List<Drink>();
    }
}

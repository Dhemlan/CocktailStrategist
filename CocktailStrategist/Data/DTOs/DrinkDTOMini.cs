namespace CocktailStrategist.Data.DTOs
{
    public class DrinkDTOMini
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Ingredients { get; set; }
    }
}

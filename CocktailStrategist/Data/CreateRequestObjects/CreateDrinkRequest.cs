namespace CocktailStrategist.Data.CreateRequestObjects
{
    public class CreateDrinkRequest
    {
        public string Name { get; set; }
        public List<Guid> Ingredients { get; set; }
    }
}

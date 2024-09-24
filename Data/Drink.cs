﻿namespace CocktailStrategist.Data
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; } = new List<Ingredient>();
    }
}
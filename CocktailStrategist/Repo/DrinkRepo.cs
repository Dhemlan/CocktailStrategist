using CocktailStrategist.Data;
using CocktailStrategist.Data.DTOs;
using CocktailStrategist.Repo.Interfaces;
using CocktailStrategist.Services;
using Microsoft.EntityFrameworkCore;

namespace CocktailStrategist.Repo
{
    public class DrinkRepo : BaseRepo<Drink>, IDrinkRepo
    {
        public DrinkRepo(AppDbContext dbContext) : base(dbContext) { }
        public async Task<Drink> GetWithRecipes(Guid id)
        {
           return  await _dbSet.Where(drink => drink.Id == id).Include(d => d.Recipes).ThenInclude(r => r.IngredientUsages).FirstAsync();
            
        }

        public async Task<IEnumerable<DrinkDTOMini>> GetAllMini()
        {
            return await _dbSet.Select(d => new DrinkDTOMini
            {
                Id = d.Id,
                Name = d.Name,
                Ingredients = d.Recipes.Where(r => r.IsDefault)
                        .SelectMany(r => r.IngredientUsages)
                        .Select(iu => iu.Ingredient.Name)
            }).ToListAsync();
        }

        public async Task<IEnumerable<DrinkDTOMini>> GetMini(Guid ingredientId)
        {
            var drinks = await _dbSet
                .Where(d => d.Recipes.Any(r => r.IngredientUsages.Any(iu => iu.IngredientId == ingredientId)))
                .Select(d => new DrinkDTOMini
                {
                    Id = d.Id,
                    Name = d.Name,
                    Ingredients = d.Recipes.Where(r => r.IsDefault)
                        .SelectMany(r => r.IngredientUsages)
                        .Select(iu => iu.Ingredient.Name)
                }).ToListAsync();
            return drinks;
        }
    }
}

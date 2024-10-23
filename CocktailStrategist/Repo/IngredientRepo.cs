using CocktailStrategist.Data;
using CocktailStrategist.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CocktailStrategist.Repo
{
    public class IngredientRepo : BaseRepo<Ingredient>, IIngredientRepo
    {

        public IngredientRepo(AppDbContext dbContext) : base(dbContext) { }

        public async Task<List<List<Ingredient>>> GetAsCategories()
        {
            throw new NotImplementedException();
           // var groups = _dbSet.GroupBy(i => i.Category, i => i, (key, group) => new { Category = key, Ingredients = group });
            //return 
        }

        public async Task<List<Ingredient>> GetMultiple(List<Guid> ingredientIds)
        {
            return await _dbSet.Where(i => ingredientIds.Contains(i.Id)).ToListAsync();
        }
    }

}

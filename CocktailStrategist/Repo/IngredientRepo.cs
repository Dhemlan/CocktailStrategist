using CocktailStrategist.Data;
using CocktailStrategist.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CocktailStrategist.Repo
{
    public class IngredientRepo : BaseRepo<Ingredient>, IIngredientRepo
    {

        public IngredientRepo(AppDbContext dbContext) : base(dbContext) { }

        public async Task<List<Ingredient>> GetMultiple(List<Guid> ingredientIds)
        {
            return await _dbSet.Where(i => ingredientIds.Contains(i.Id)).ToListAsync();
        }

        public Ingredient GetWithDrinks(Guid id)
        {
            return _dbSet.Where(x => x.Id == id).Include(i => i.Drinks).ThenInclude(d => d.Ingredients).First();
        }
    }

}

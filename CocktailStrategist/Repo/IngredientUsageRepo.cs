using CocktailStrategist.Data;
using CocktailStrategist.Data.DTOs;
using CocktailStrategist.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CocktailStrategist.Repo
{
    public class IngredientUsageRepo : IIngredientUsageRepo
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<IngredientUsage> _dbSet;

        public IngredientUsageRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<IngredientUsage>();
        }
        public IngredientUsage Create(IngredientUsage ingredientUsage)
        {
            _dbSet.Add(ingredientUsage);
            return ingredientUsage;
        }

        public async Task<IngredientUsage> Get(IngredientUsage ingredientUsage)
        {
            return await _dbSet.FirstOrDefaultAsync(iu => iu.IngredientId == ingredientUsage.IngredientId && iu.Measurement == ingredientUsage.Measurement && iu.Amount == ingredientUsage.Amount) ??
                Create(ingredientUsage);
        }
    }
}

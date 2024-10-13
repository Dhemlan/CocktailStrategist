using CocktailStrategist.Data;
using CocktailStrategist.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.CompilerServices;

namespace CocktailStrategist.Repo
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    { 
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        
        public BaseRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task<T?> Get(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public void Update(T entity)
        {
            // Todo: research change tracker functioning
            // This works but is it appropriate?
            _dbContext.ChangeTracker.Clear();
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

        }

        public async Task<T?> Delete(Guid id)
        {
            T? existing = await _dbSet.FindAsync(id);
            return existing != null ? _dbSet.Remove(existing).Entity : null;
        }

        public async Task SaveAsync()
        {
           await _dbContext.SaveChangesAsync();
        }

       
    }
}

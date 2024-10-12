using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CocktailStrategist.Repo.Interfaces
{
    public interface IBaseRepo<T> where T : class
    {
        void Create(T entity); 
        Task<IEnumerable<T>> GetAll();
        Task<T?> Get(Guid id);
        Task<T?> Update(T entity, Guid id);
        Task<T?> Delete(Guid id);
        Task SaveAsync();
    }
}

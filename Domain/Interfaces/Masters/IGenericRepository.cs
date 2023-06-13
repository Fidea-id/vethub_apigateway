using System.Linq.Expressions;

namespace Domain.Interfaces.Masters
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> FindBy(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task<bool> Any(Expression<Func<T, bool>> expression);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}

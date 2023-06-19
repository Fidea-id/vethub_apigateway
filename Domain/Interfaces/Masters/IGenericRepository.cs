using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces.Masters
{
    public interface IGenericRepository<T, TFilter>
        where T : BaseEntity
        where TFilter : BaseEntityFilter
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllActive();
        Task<bool> Any(Expression<Func<T, bool>> expression);
        Task<T> FindBy(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAllAsQuery();
        Task<IEnumerable<T>> GetWithFilter(object filter);
        Task<IEnumerable<T>> GetWithTFilter(TFilter? filter);
        Task<int> CountWithFilter(object filter);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}

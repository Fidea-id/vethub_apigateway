using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IGenericRepository<T, TFilter> where T : class where TFilter : BaseEntityFilter

    {
        Task<T> GetById(string dbName, int id);
        Task<IEnumerable<T>> GetAll(string dbName);
        Task<DataResultDTO<T>> GetByFilter(string dbName, TFilter filters);
        Task<IEnumerable<T>> GetAllActive(string dbName);
        Task<IEnumerable<T>> WhereQuery(string dbName, string query);
        Task<T> WhereFirstQuery(string dbName, string query);
        Task<bool> AnyQuery(string dbName, string query);
        Task<int> Add(string dbName, T entity);
        Task AddRange(string dbName, IEnumerable<T> entity);
        Task Update(string dbName, T entity);
        Task Remove(string dbName, int id);
        Task UpdateRange(string dbName, IEnumerable<T> entity);
        Task RemoveRange(string dbName, IEnumerable<T> entity);
        Task<int> CountWithFilter(string dbName, TFilter filter);
        Task<int> Count(string dbName);
        Task<int> CountWithQuery(string dbName, string query);
        Task<CardDashboard> CountToCard(string dbName, string date, string query);
        Task<int> Sum(string dbName, string columnName);
        Task<int> SumWithQuery(string dbName, string columnName, string query);
        Task<double> SumDoubleWithQuery(string dbName, string columnName, string query);
    }
}

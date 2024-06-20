using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IEventLogRepository : IGenericRepository<EventLogs, EventLogFilter>
    {
        Task<EventLogs> AddEventLogByParams(string dbName, int userId, int recordId, string methodName, MethodType methodType, string objectName, string? detail = null);
        Task<DataResultDTO<EventLogs>> GetEventLogByObjectId(string dbName, int recordId, string objectName);
        Task<DataResultDTO<EventLogs>> GetEventLogByObjectUser(string dbName, int userId, string objectName);
    }
}

using Domain.Entities.Filters;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IClinicConfigRepository : IGenericRepository<ClinicConfig, NameBaseEntityFilter>
    {
        Task<int> AddConfig(string dbName, ClinicConfig config);
        Task UpdateConfig(string dbName, ClinicConfig config);
        Task<string> GetConfigValue(string dbName, string key);
        Task<ClinicConfig> GetConfigByKey(string dbName, string key);
    }
}

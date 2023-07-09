using Newtonsoft.Json.Linq;

namespace Domain.Interfaces.Clients
{
    public interface IGenerateTableRepository
    {
        Task GenerateAllTable(string dbName);
        Task<bool> CheckInitSchema(string dbName, string init);
        Task SetInitSchema(string dbName, string init);
        Task UpdateTable(string dbName, int version);
        Task GenerateTableField(string dbName, JObject fields);
    }
}

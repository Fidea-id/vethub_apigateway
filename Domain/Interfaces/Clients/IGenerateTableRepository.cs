namespace Domain.Interfaces.Clients
{
    public interface IGenerateTableRepository
    {
        Task GenerateAllTable(string dbName);
        Task UpdateTable(string dbName, int version);
        Task GenerateTableField(string dbName, Dictionary<string, object> fields);
    }
}

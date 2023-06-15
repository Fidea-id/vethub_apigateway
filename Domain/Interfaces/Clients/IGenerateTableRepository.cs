namespace Domain.Interfaces.Clients
{
    public interface IGenerateTableRepository
    {
        Task GenerateAllTable(string dbName);
        Task GenerateTableField(string dbName, Dictionary<string, object> fields);
    }
}

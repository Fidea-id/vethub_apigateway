using System;
using System.Threading.Tasks;

namespace Application.Services.Contracts
{
    public interface IMedicalRecordNoteMigrationService
    {
        Task<bool> ValidateTenantDatabaseAsync(string dbName, string authToken);
        void LogAuditInfo(string userId, string userEmail, string dbName, int batchCount, DateTime triggeredAt);
        string EnqueueMigrationJob(string dbName, int batchCount);
    }
}

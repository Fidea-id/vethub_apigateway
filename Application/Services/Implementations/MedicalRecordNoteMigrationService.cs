using Application.Services.Contracts;
using Application.Jobs;
using Domain.Entities;
using Domain.Entities.Responses.Masters;
using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Implementations
{
    public class MedicalRecordNoteMigrationService : IMedicalRecordNoteMigrationService
    {
        private readonly IRestAPIService _restAPIService;
        private readonly ILogger<MedicalRecordNoteMigrationService> _logger;

        public MedicalRecordNoteMigrationService(
            IRestAPIService restAPIService,
            ILogger<MedicalRecordNoteMigrationService> logger)
        {
            _restAPIService = restAPIService;
            _logger = logger;
        }

        public async Task<bool> ValidateTenantDatabaseAsync(string dbName, string authToken)
        {
            try
            {
                var responseUserOwner = await _restAPIService.GetResponse<IEnumerable<UserDataResponse>>(
                    APIType.Master, "Auth/User/Entity", authToken);
                
                if (responseUserOwner == null) return false;

                return responseUserOwner.Any(u => string.Equals(u.Entity, dbName, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[MigrationService] Error validating tenant database: {dbName}");
                return false;
            }
        }

        public void LogAuditInfo(string userId, string userEmail, string dbName, int batchCount, DateTime triggeredAt)
        {
            _logger.LogInformation(
                $"[MigrationAudit] Migration triggered." + Environment.NewLine +
                $"- UserId: {userId}" + Environment.NewLine +
                $"- UserEmail: {userEmail}" + Environment.NewLine +
                $"- DatabaseName: {dbName}" + Environment.NewLine +
                $"- BatchCount: {batchCount}" + Environment.NewLine +
                $"- TriggeredAt: {triggeredAt:yyyy-MM-dd HH:mm:ss UTC}"
            );
        }

        public string EnqueueMigrationJob(string dbName, int batchCount)
        {
            var jobId = BackgroundJob.Enqueue<MedicalRecordNoteMigrationJob>(
                job => job.ExecuteAsync(dbName, batchCount, null));
            return jobId;
        }
    }
}

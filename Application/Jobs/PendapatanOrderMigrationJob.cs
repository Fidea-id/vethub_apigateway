using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.Models.Masters;
using Domain.Entities.Responses.Masters;
using Hangfire.Server;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Jobs
{
    public class PendapatanOrderMigrationJob
    {
        private readonly IRestAPIService _restAPIService;
        private readonly ILogger<PendapatanOrderMigrationJob> _logger;

        public PendapatanOrderMigrationJob(IRestAPIService restAPIService, ILogger<PendapatanOrderMigrationJob> logger)
        {
            _restAPIService = restAPIService;
            _logger = logger;
        }

        public async Task ExecuteAsync(string authToken, PerformContext? context = null)
        {
            _logger.LogInformation("[PendapatanOrderMigrationJob] Starting job...");

            try
            {
                // 1. Get clinics/users from MasterAPI
                var responseUserOwner = await _restAPIService.GetResponse<IEnumerable<UserDataResponse>>(APIType.Master, "Auth/User/Entity", authToken);

                if (responseUserOwner == null || !responseUserOwner.Any())
                {
                    _logger.LogWarning("[PendapatanOrderMigrationJob] No clinics found to process.");
                    return;
                }

                foreach (var item in responseUserOwner)
                {
                    try
                    {
                        _logger.LogInformation($"[PendapatanOrderMigrationJob] Processing clinic entity={item.Entity}");

                        // Create system JWT token scoped to tenant
                        var systemUser = new Users
                        {
                            Id = 0,
                            Name = "System Migrator",
                            Email = "system-migration@vethub.id",
                            Roles = "Superadmin",
                            Entity = item.Entity,
                            IsVerified = true
                        };
                        var token = JwtUtil.SetSessionToken(systemUser);
                        string authHeader = "Bearer " + token;

                        // Call Client API migrate endpoint with extended timeout
                        var timeout = TimeSpan.FromMinutes(10);
                        var result = await _restAPIService.PostResponseWithCTS<object>(APIType.Client, "ChartOfAccounts/MigratePendapatanOrder", string.Empty, authHeader, timeout);

                        _logger.LogInformation($"[PendapatanOrderMigrationJob] Clinic {item.Entity} processed successfully.");
                    }
                    catch (Exception exInner)
                    {
                        _logger.LogError(exInner, $"[PendapatanOrderMigrationJob] Failed while processing clinic entity={item.Entity}");
                        // continue to next clinic
                    }
                }

                _logger.LogInformation("[PendapatanOrderMigrationJob] Job completed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[PendapatanOrderMigrationJob] Fatal error in ExecuteAsync");
                throw;
            }
        }
    }
}

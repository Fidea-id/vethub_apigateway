using Application.Services.Contracts;
using Domain.Entities;
using Domain.Entities.DTOs.Clients;
using Domain.Entities.Models.Masters;
using Domain.Entities.Responses.Masters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Jobs
{
    public class ClinicServiceUsageJob
    {
        private readonly IRestAPIService _restAPIService;
        private readonly ILogger<ClinicServiceUsageJob> _logger;

        public ClinicServiceUsageJob(IRestAPIService restAPIService, ILogger<ClinicServiceUsageJob> logger)
        {
            _restAPIService = restAPIService;
            _logger = logger;
        }

        public async Task ExecuteAsync(string authToken)
        {
            try
            {
                _logger.LogInformation("[ClinicServiceUsageJob] Starting job...");

                // 1. Ambil owner users dari MasterAPI
                _logger.LogInformation("[ClinicServiceUsageJob] Fetching User/Entity from MasterAPI");
                var responseUserOwner = await _restAPIService.GetResponse<IEnumerable<UserDataResponse>>(
                    APIType.Master, "Auth/User/Entity", authToken);

                if (responseUserOwner == null || !responseUserOwner.Any())
                {
                    _logger.LogWarning("[ClinicServiceUsageJob] ResponseUserOwner is NULL or empty");
                    return;
                }

                // 2. Loop userOwner -> ambil data dari ClientAPI
                foreach (var item in responseUserOwner)
                {
                    var reports = new List<ClinicServiceUsage>();
                    try
                    {
                        _logger.LogInformation($"[ClinicServiceUsageJob] Processing clinic entity={item.Entity}, userId={item.Id}");

                        var clinicDataReport = await _restAPIService.GetResponse<IEnumerable<ClinicServiceUsageDTO>>(
                            APIType.Client, $"Data/ClinicServiceUsageReports/{item.Entity}", authToken);

                        if (clinicDataReport == null)
                        {
                            _logger.LogWarning($"[ClinicServiceUsageJob] ClientAPI returned NULL for {item.Entity}");
                            continue;
                        }

                        _logger.LogInformation($"[ClinicServiceUsageJob] Got clinic report for {item.Entity}");

                        // Build report
                        foreach (var itemInside in clinicDataReport)
                        {
                            var newClinicServiceUsageReport = new ClinicServiceUsage
                            {
                                Entity = item.Entity,
                                ClinicName = itemInside.ClinicName,
                                ServiceId = itemInside.ServiceId,
                                DurationText = itemInside.DurationText,
                                Price = itemInside.Price,
                                ServiceName = itemInside.ServiceName,
                                TotalUsage = itemInside.TotalUsage, 
                                IsActive = true,
                                UpdatedAt = item.CreatedAt,
                                CreatedAt = item.CreatedAt
                            };
                            reports.Add(newClinicServiceUsageReport);
                        }

                        // 3. Bulk POST ke MasterAPI
                        if (reports.Any())
                        {
                            _logger.LogInformation($"[ClinicServiceUsageJob] Posting {reports.Count} reports to MasterAPI");
                            var postResponse = await _restAPIService.PostResponse<object>(
                                APIType.Master, "Data/ClinicServiceUsage/", JsonConvert.SerializeObject(reports), authToken);

                            if (postResponse == null)
                                _logger.LogWarning("[ClinicServiceUsageJob] MasterAPI response NULL after bulk post");
                        }
                        else
                        {
                            _logger.LogWarning("[ClinicServiceUsageJob] No reports collected, skipping MasterAPI post");
                        }
                    }
                    catch (Exception exInner)
                    {
                        _logger.LogError(exInner, $"[ClinicServiceUsageJob] Failed while processing clinic entity={item.Entity}");
                        // lanjut ke next clinic
                    }
                }
                _logger.LogInformation("[ClinicServiceUsageJob] Job completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ClinicServiceUsageJob] Fatal error in ExecuteAsync");
                throw;
            }
        }
    }
}

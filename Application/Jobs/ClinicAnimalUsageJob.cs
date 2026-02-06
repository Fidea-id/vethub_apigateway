using Application.Services.Contracts;
using Domain.Entities;
using Domain.Entities.DTOs.Clients;
using Domain.Entities.Models.Masters;
using Domain.Entities.Responses.Masters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Jobs
{
    public class ClinicAnimalUsageJob
    {
        private readonly IRestAPIService _restAPIService;
        private readonly ILogger<ClinicAnimalUsageJob> _logger;

        public ClinicAnimalUsageJob(IRestAPIService restAPIService, ILogger<ClinicAnimalUsageJob> logger)
        {
            _restAPIService = restAPIService;
            _logger = logger;
        }

        public async Task ExecuteAsync(string authToken)
        {
            try
            {
                _logger.LogInformation("[ClinicAnimalUsageJob] Starting job...");

                // 1. Ambil owner users dari MasterAPI
                _logger.LogInformation("[ClinicAnimalUsageJob] Fetching User/Entity from MasterAPI");
                var responseUserOwner = await _restAPIService.GetResponse<IEnumerable<UserDataResponse>>(
                    APIType.Master, "Auth/User/Entity", authToken);

                if (responseUserOwner == null || !responseUserOwner.Any())
                {
                    _logger.LogWarning("[ClinicAnimalUsageJob] ResponseUserOwner is NULL or empty");
                    return;
                }

                // 2. Loop userOwner -> ambil data dari ClientAPI
                foreach (var item in responseUserOwner)
                {
                    var reports = new List<ClinicAnimalUsage>();

                    try
                    {
                        _logger.LogInformation($"[ClinicAnimalUsageJob] Processing clinic entity={item.Entity}, userId={item.Id}");

                        var clinicDataReport = await _restAPIService.GetResponse<IEnumerable<ClinicAnimalUsageDTO>>(
                            APIType.Client, $"Data/ClinicAnimalUsageReports/{item.Entity}", authToken);

                        if (clinicDataReport == null)
                        {
                            _logger.LogWarning($"[ClinicAnimalUsageJob] ClientAPI returned NULL for {item.Entity}");
                            continue;
                        }

                        _logger.LogInformation($"[ClinicAnimalUsageJob] Got clinic report for {item.Entity}");

                        // Build report
                        foreach (var itemInside in clinicDataReport)
                        {
                            var newClinicAnimalUsage = new ClinicAnimalUsage
                            {
                                Entity = item.Entity,
                                ClinicName = itemInside.ClinicName,
                                AnimalId = itemInside.AnimalId,
                                Name = itemInside.Name,
                                Count = itemInside.Count,
                                IsActive = true,
                                UpdatedAt = item.CreatedAt,
                                CreatedAt = item.CreatedAt
                            };
                            reports.Add(newClinicAnimalUsage);
                        }

                        // 3. Bulk POST ke MasterAPI
                        if (reports.Any())
                        {
                            _logger.LogInformation($"[ClinicAnimalUsageJob] Posting {reports.Count} reports to MasterAPI");
                            var postResponse = await _restAPIService.PostResponse<object>(
                                APIType.Master, "Data/ClinicAnimalUsage/", JsonConvert.SerializeObject(reports), authToken);

                            if (postResponse == null)
                                _logger.LogWarning("[ClinicAnimalUsageJob] MasterAPI response NULL after bulk post");
                        }
                        else
                        {
                            _logger.LogWarning("[ClinicAnimalUsageJob] No reports collected, skipping MasterAPI post");
                        }
                    }
                    catch (Exception exInner)
                    {
                        _logger.LogError(exInner, $"[ClinicAnimalUsageJob] Failed while processing clinic entity={item.Entity}");
                        // lanjut ke next clinic
                    }
                }

                _logger.LogInformation("[ClinicAnimalUsageJob] Job completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ClinicAnimalUsageJob] Fatal error in ExecuteAsync");
                throw;
            }
        }
    }
}

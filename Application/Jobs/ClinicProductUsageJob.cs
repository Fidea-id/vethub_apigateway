using Application.Services.Contracts;
using Domain.Entities;
using Domain.Entities.DTOs.Clients;
using Domain.Entities.Models.Masters;
using Domain.Entities.Responses.Masters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Jobs
{
    public class ClinicProductUsageJob
    {
        private readonly IRestAPIService _restAPIService;
        private readonly ILogger<ClinicProductUsageJob> _logger;

        public ClinicProductUsageJob(IRestAPIService restAPIService, ILogger<ClinicProductUsageJob> logger)
        {
            _restAPIService = restAPIService;
            _logger = logger;
        }

        public async Task ExecuteAsync(string authToken)
        {
            try
            {
                _logger.LogInformation("[ClinicProductUsageJob] Starting job...");

                // 1. Ambil owner users dari MasterAPI
                _logger.LogInformation("[ClinicProductUsageJob] Fetching User/Entity from MasterAPI");
                var responseUserOwner = await _restAPIService.GetResponse<IEnumerable<UserDataResponse>>(
                    APIType.Master, "Auth/User/Entity", authToken);

                if (responseUserOwner == null || !responseUserOwner.Any())
                {
                    _logger.LogWarning("[ClinicProductUsageJob] ResponseUserOwner is NULL or empty");
                    return;
                }

                // 2. Loop userOwner -> ambil data dari ClientAPI
                foreach (var item in responseUserOwner)
                {
                    var reports = new List<ClinicProductUsage>();
                    try
                    {
                        _logger.LogInformation($"[ClinicProductUsageJob] Processing clinic entity={item.Entity}, userId={item.Id}");

                        var clinicDataReport = await _restAPIService.GetResponse<IEnumerable<ClinicProductUsageDTO>>(
                            APIType.Client, $"Data/ClinicProductUsageReports/{item.Entity}", authToken);

                        if (clinicDataReport == null)
                        {
                            _logger.LogWarning($"[ClinicProductUsageJob] ClientAPI returned NULL for {item.Entity}");
                            continue;
                        }

                        _logger.LogInformation($"[ClinicProductUsageJob] Got clinic report for {item.Entity}");

                        // Build report
                        foreach (var itemInside in clinicDataReport)
                        {
                            var newClinicProductUsageReport = new ClinicProductUsage
                            {
                                Entity = item.Entity,
                                ClinicName = itemInside.ClinicName,
                                ProductId = itemInside.ProductId,
                                ProductName = itemInside.ProductName,
                                ProductAliasName = itemInside.ProductAliasName,
                                ProductCategory = itemInside.ProductCategory,
                                ProductVolume = itemInside.ProductVolume,
                                Price = itemInside.Price,
                                TotalUsage = itemInside.TotalUsage, 
                                IsActive = true,
                                UpdatedAt = item.CreatedAt,
                                CreatedAt = item.CreatedAt
                            };
                            reports.Add(newClinicProductUsageReport);
                        }

                        // 3. Bulk POST ke MasterAPI
                        if (reports.Any())
                        {
                            _logger.LogInformation($"[ClinicProductUsageJob] Posting {reports.Count} reports to MasterAPI");
                            var postResponse = await _restAPIService.PostResponse<object>(
                                APIType.Master, "Data/ClinicProductUsage/", JsonConvert.SerializeObject(reports), authToken);

                            if (postResponse == null)
                                _logger.LogWarning("[ClinicProductUsageJob] MasterAPI response NULL after bulk post");
                        }
                        else
                        {
                            _logger.LogWarning("[ClinicProductUsageJob] No reports collected, skipping MasterAPI post");
                        }
                    }
                    catch (Exception exInner)
                    {
                        _logger.LogError(exInner, $"[ClinicProductUsageJob] Failed while processing clinic entity={item.Entity}");
                        // lanjut ke next clinic
                    }
                }

                _logger.LogInformation("[ClinicProductUsageJob] Job completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ClinicProductUsageJob] Fatal error in ExecuteAsync");
                throw;
            }
        }
    }
}

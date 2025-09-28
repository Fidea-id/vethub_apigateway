using Application.Services.Contracts;
using Domain.Entities.DTOs.Clients;
using Domain.Entities.Models.Masters;
using Domain.Entities.Responses.Masters;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Jobs
{
    public class ClinicReportsJob
    {
        private readonly IRestAPIService _restAPIService;
        private readonly ILogger<ClinicReportsJob> _logger;

        public ClinicReportsJob(IRestAPIService restAPIService, ILogger<ClinicReportsJob> logger)
        {
            _restAPIService = restAPIService;
            _logger = logger;
        }

        public async Task ExecuteAsync(string authToken)
        {
            try
            {
                _logger.LogInformation("[ClinicReportsJob] Starting job...");

                // 1. Ambil owner users dari MasterAPI
                _logger.LogInformation("[ClinicReportsJob] Fetching User/Entity from MasterAPI");
                var responseUserOwner = await _restAPIService.GetResponse<IEnumerable<UserDataResponse>>(
                    APIType.Master, "Auth/User/Entity", authToken);

                if (responseUserOwner == null || !responseUserOwner.Any())
                {
                    _logger.LogWarning("[ClinicReportsJob] ResponseUserOwner is NULL or empty");
                    return;
                }

                var reports = new List<ClinicReports>();

                // 2. Loop userOwner -> ambil data dari ClientAPI
                foreach (var item in responseUserOwner)
                {
                    try
                    {
                        _logger.LogInformation($"[ClinicReportsJob] Processing clinic entity={item.Entity}, userId={item.Id}");

                        var clinicDataReport = await _restAPIService.GetResponse<ClinicReportsClientDTO>(
                            APIType.Client, $"Data/ClinicReports/{item.Entity}", authToken);

                        if (clinicDataReport == null)
                        {
                            _logger.LogWarning($"[ClinicReportsJob] ClientAPI returned NULL for {item.Entity}");
                            continue;
                        }

                        _logger.LogInformation($"[ClinicReportsJob] Got clinic report for {item.Entity}: " +
                            $"Patients={clinicDataReport.TotalPatients}, " +
                            $"Appointments={clinicDataReport.TotalAppointments}, " +
                            $"Revenue={clinicDataReport.TotalRevenue}");

                        // Build report
                        var newClinicReport = new ClinicReports
                        {
                            Entity = item.Entity,
                            ClinicName = clinicDataReport.ClinicName,
                            TotalPatients = clinicDataReport.TotalPatients,
                            TotalAppointments = clinicDataReport.TotalAppointments,
                            TotalMedicalRecords = clinicDataReport.TotalMedicalRecords,
                            TotalRevenue = clinicDataReport.TotalRevenue,
                            LastActivity = clinicDataReport.LastActivity,
                            JoinDate = item.CreatedAt,
                            CreatedAt = item.CreatedAt
                        };

                        reports.Add(newClinicReport);
                    }
                    catch (Exception exInner)
                    {
                        _logger.LogError(exInner, $"[ClinicReportsJob] Failed while processing clinic entity={item.Entity}");
                        // lanjut ke next clinic
                    }
                }

                // 3. Bulk POST ke MasterAPI
                if (reports.Any())
                {
                    _logger.LogInformation($"[ClinicReportsJob] Posting {reports.Count} reports to MasterAPI");
                    var postResponse = await _restAPIService.PostResponse<object>(
                        APIType.Master, "Data/ClinicReports/", JsonConvert.SerializeObject(reports), authToken);

                    if (postResponse == null)
                        _logger.LogWarning("[ClinicReportsJob] MasterAPI response NULL after bulk post");
                }
                else
                {
                    _logger.LogWarning("[ClinicReportsJob] No reports collected, skipping MasterAPI post");
                }

                _logger.LogInformation("[ClinicReportsJob] Job completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ClinicReportsJob] Fatal error in ExecuteAsync");
                throw;
            }
        }
    }
}

using Application.Jobs;
using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Entities.Models.Masters;
using Domain.Entities.Requests.Masters;
using Domain.Entities.Responses.Masters;
using Domain.Entities.Responses;
using Domain.Entities.Responses.Clients;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Clinics = Domain.Entities.Models.Clients.Clinics;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Superadmin")]
    public class AdminClinicsController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        private readonly ILogger<AdminClinicsController> _logger;

        public AdminClinicsController(IRestAPIService restAPIService,
            ILoggerFactory loggerFactory)
        {
            _restAPIService = restAPIService;
            _logger = loggerFactory.CreateLogger<AdminClinicsController>();
        }

        [HttpGet("DashboardAdminData")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetDashboardAdminData()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<DashboardAdminResponse>(APIType.Master, "Data/DashboardData", authToken);

                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("UserDemo")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetUserDemo()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<UserDemo>>(APIType.Master, "Subscriptions/UserDemo", authToken);

                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetClinicList([FromQuery] BaseEntityFilter? filter)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var searchTerm = filter?.Search?.Trim();
                var hasSearch = !string.IsNullOrWhiteSpace(searchTerm);

                // The admin grid searches across the composed clinic rows, not just the upstream owner list.
                // When a search term exists, fetch the full matching owner set and apply the final filter
                // after the clinic details are hydrated so nested columns such as clinic name/city/phone work.
                var requestFilter = hasSearch
                    ? new BaseEntityFilter
                    {
                        Id = filter?.Id,
                        IsActive = filter?.IsActive,
                        CreatedAt = filter?.CreatedAt,
                        UpdatedAt = filter?.UpdatedAt,
                        SortProp = string.IsNullOrWhiteSpace(filter?.SortProp) ? "Id" : filter?.SortProp,
                        SortMode = string.IsNullOrWhiteSpace(filter?.SortMode) ? "ASC" : filter?.SortMode
                    }
                    : new BaseEntityFilter
                    {
                        Id = filter?.Id,
                        IsActive = filter?.IsActive,
                        CreatedAt = filter?.CreatedAt,
                        UpdatedAt = filter?.UpdatedAt,
                        Search = filter?.Search,
                        SortProp = string.IsNullOrWhiteSpace(filter?.SortProp) ? "Id" : filter?.SortProp,
                        SortMode = string.IsNullOrWhiteSpace(filter?.SortMode) ? "ASC" : filter?.SortMode,
                        Skip = filter?.Skip,
                        Take = filter?.Take
                    };

                var response = await _restAPIService.GetResponseFilter<DataResultDTO<UserDataResponse>, BaseEntityFilter>(APIType.Master, "Auth/User/Entity", authToken, requestFilter);
                var tasks = response.Data.Select(async item =>
                {
                    try
                    {
                        var responseClinic = await _restAPIService.GetResponse<Clinics>(APIType.Client, "Data/ClinicsEntity/" + item.Entity, authToken);
                        var responseLatestBill = await _restAPIService.GetResponse<UserBillResponse>(APIType.Master, "BillPayments/Latest/" + item.Id, authToken);
                        string statusBill = "On Going";
                        if (responseLatestBill.EndDate < DateTime.Now || responseLatestBill.Status == "Expired")
                        {
                            statusBill = "Expired";
                        }

                        return new ClientClinicListResponse
                        {
                            OwnerData = new ClientOwnerResponse
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Email = item.Email,
                                IsVerified = item.IsVerified
                            },
                            JoinDate = item.CreatedAt,
                            Id = item.Id,
                            ClinicData = new ClientClinicResponse
                            {
                                Id = responseClinic.Id,
                                Name = responseClinic.Name,
                                Address = responseClinic.Address,
                                City = responseClinic.City,
                                Description = responseClinic.Description,
                                Email = responseClinic.Email,
                                Logo = responseClinic.Logo,
                                MapUrl = responseClinic.MapUrl,
                                PhoneNumber = responseClinic.PhoneNumber,
                                State = responseClinic.State,
                                WebUrl = responseClinic.WebUrl
                            },
                            StartDate = responseLatestBill.StartDate,
                            Status = statusBill,
                            EndDate = responseLatestBill.EndDate
                        };
                    }
                    catch
                    {
                        _logger.LogInformation("User Clinic of " + item.Email + " not found");
                        return null;
                    }
                });

                var dataList = (await Task.WhenAll(tasks)).Where(x => x != null).Select(x => x!).ToList();

                var filteredCount = response.TotalData;

                if (hasSearch)
                {
                    dataList = dataList
                        .Where(item =>
                            Contains(item.ClinicData?.Name, searchTerm) ||
                            Contains(item.ClinicData?.City, searchTerm) ||
                            Contains(item.ClinicData?.PhoneNumber, searchTerm) ||
                            Contains(item.ClinicData?.Email, searchTerm) ||
                            Contains(item.OwnerData?.Name, searchTerm) ||
                            Contains(item.OwnerData?.Email, searchTerm) ||
                            Contains(item.Status, searchTerm))
                        .ToList();

                    filteredCount = dataList.Count;

                    if (filter?.Skip.HasValue == true && filter.Skip.Value > 0)
                    {
                        dataList = dataList.Skip(filter.Skip.Value).ToList();
                    }

                    if (filter?.Take.HasValue == true && filter.Take.Value > 0)
                    {
                        dataList = dataList.Take(filter.Take.Value).ToList();
                    }
                }

                var totalData = hasSearch ? filteredCount : response.TotalData;
                return ResponseUtil.CustomOk(new DataResultDTO<ClientClinicListResponse>
                {
                    Data = dataList,
                    TotalData = totalData
                }, 200, totalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("RevokeUserSession")]
        public async Task<IActionResult> RevokeUserSession([FromBody] RevokeUserSessionRequest request)
        {
            string? authToken = HttpContext.Request.Headers["Authorization"];
            var response = await _restAPIService.PostResponse<SessionVersionResponse>(
                APIType.Master,
                "Auth/Session/Revoke",
                System.Text.Json.JsonSerializer.Serialize(request),
                authToken);

            return ResponseUtil.CustomOk(response, 200);
        }

        private static bool Contains(string? value, string? searchTerm)
        {
            return !string.IsNullOrWhiteSpace(value)
                && !string.IsNullOrWhiteSpace(searchTerm)
                && value.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
        }

        [HttpGet("Detail/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetClinicDetail(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<UserDataResponse>(APIType.Master, "Auth/User/Entity/" + id, authToken);
                var responseClinic = await _restAPIService.GetResponse<Clinics>(APIType.Client, "Data/ClinicsEntity/" + response.Entity, authToken);
                var responseClinicPatients = await _restAPIService.GetResponse<BaseAPIResponse<int>>(APIType.Client, "Patients/CountActive/" + response.Entity, authToken);
                var responseLatestBill = await _restAPIService.GetResponse<UserBillResponse>(APIType.Master, "BillPayments/Latest/" + id, authToken);
                var responseSubscriptions = await _restAPIService.GetResponse<SubscriptionResponse>(APIType.Master, "Subscriptions/" + responseLatestBill.SubscriptionId, authToken);

                var clinicData = new ClientClinicResponse
                {
                    Id = responseClinic.Id,
                    Name = responseClinic.Name,
                    Address = responseClinic.Address,
                    City = responseClinic.City,
                    Description = responseClinic.Description,
                    Email = responseClinic.Email,
                    Logo = responseClinic.Logo,
                    MapUrl = responseClinic.MapUrl,
                    PhoneNumber = responseClinic.PhoneNumber,
                    State = responseClinic.State,
                    WebUrl = responseClinic.WebUrl
                };

                var clinicStatistic = new ClientClinicStatisticResponse
                {
                    Id = responseClinic.Id,
                    ClientsTotal = responseClinicPatients.Data,
                    ClientsLimit = responseSubscriptions.PatientQuota
                };

                var data = new ClientClinicDetailResponse();
                var ownerData = new ClientOwnerResponse
                {
                    Id = response.Id,
                    Name = response.Name,
                    Email = response.Email
                };
                data.OwnerData = ownerData;
                data.JoinDate = response.CreatedAt;
                data.Id = response.Id;
                data.ClinicData = clinicData;
                data.LatestBillData = responseLatestBill;
                data.ClinicStatisticData = clinicStatistic;

                return ResponseUtil.CustomOk(data, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("HistoryBills/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetUserHistoryBill(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<UserBillResponse>>(APIType.Master, "BillPayments/UserHistory?clientId=" + id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("ClinicUsers/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetClinicUsers(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<UserDataResponse>>(APIType.Master, "Auth/UserEntity/" + id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("ClinicReports")]
        public async Task<IActionResult> GetClinicReports()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<ClinicReports>>(APIType.Master, "Data/ClinicReports", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("ClinicReports/Generate")]
        public async Task<IActionResult> GenerateClinicReports()
        {
            string? authToken = HttpContext.Request.Headers["Authorization"];
            RecurringJob.AddOrUpdate<ClinicReportsJob>(
                job => job.ExecuteAsync(authToken), // authToken bisa null kalau scheduled
                "1 0 * * *", // setiap jam 00:01
                TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta") // GMT+7
            );
            return Accepted(new { message = "ClinicReports job queued is started to Hangfire" });
        }

        [HttpGet("ClinicServiceUsage")]
        public async Task<IActionResult> GetClinicServiceUsage()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<ClinicServiceUsage>>(APIType.Master, "Data/ClinicServiceUsage", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        [HttpGet("ClinicProductUsage")]
        public async Task<IActionResult> GetClinicProductUsage()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<ClinicProductUsage>>(APIType.Master, "Data/ClinicProductUsage", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        [HttpGet("ClinicAnimalUsage")]
        public async Task<IActionResult> GetClinicAnimalUsage()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<ClinicAnimalUsage>>(APIType.Master, "Data/ClinicAnimalUsage", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("ClinicUsageReport/Generate")]
        public async Task<IActionResult> GenerateClinicUsageReports()
        {
            string? authToken = HttpContext.Request.Headers["Authorization"];
            //
            RecurringJob.AddOrUpdate<ClinicServiceUsageJob>(
                job => job.ExecuteAsync(authToken), // authToken bisa null kalau scheduled
                "10 2 * * *", // setiap jam 02:10
                TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta") // GMT+7
            );
            //
            RecurringJob.AddOrUpdate<ClinicProductUsageJob>(
                job => job.ExecuteAsync(authToken), // authToken bisa null kalau scheduled
                "10 2 * * *", // setiap jam 02:10
                TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta") // GMT+7
            );
            //
            RecurringJob.AddOrUpdate<ClinicAnimalUsageJob>(
                job => job.ExecuteAsync(authToken), // authToken bisa null kalau scheduled
                "10 2 * * *", // setiap jam 02:10
                TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta") // GMT+7
            );
            return Accepted(new { message = "ClinicUsageReports job queued is started to Hangfire" });
        }

        [HttpPost("MigratePendapatanOrder/Enqueue")]
        public async Task<IActionResult> EnqueuePendapatanOrderMigration()
        {
            try
            {
                string? authToken = HttpContext.Request.Headers["Authorization"];
                // Enqueue background job with Hangfire
                var jobId = BackgroundJob.Enqueue<PendapatanOrderMigrationJob>(
                    job => job.ExecuteAsync(authToken, null)
                );
                return Accepted(new { message = "PendapatanOrder migration job queued", jobId = jobId });
            }
            catch
            {
                throw;
            }
        }

        //[HttpGet]
        //[ResponseCache(Duration = 60)] // Cache response for 60 seconds
        //public async Task<IActionResult> GetClinic([FromQuery] ClinicsFilter filter)
        //{
        //    try
        //    {
        //        //Get the AuthToken
        //        string? authToken = HttpContext.Request.Headers["Authorization"];
        //        var response = await _restAPIService.GetResponseFilter<DataResultDTO<ClinicsEntity>, ClinicsFilter>(APIType.Master, "Clinics", authToken, filter);
        //        return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //[HttpGet("{id}")]
        //[ResponseCache(Duration = 60)] // Cache response for 60 seconds
        //public async Task<IActionResult> GetClinicById(int id)
        //{
        //    try
        //    {
        //        //Get the AuthToken
        //        string? authToken = HttpContext.Request.Headers["Authorization"];
        //        var response = await _restAPIService.GetResponse<ClinicsEntity>(APIType.Master, $"Clinics/{id}", authToken);
        //        return ResponseUtil.CustomOk(response, 200);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}

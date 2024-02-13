using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.Models.Clients;
using Domain.Entities.Models.Masters;
using Domain.Entities.Responses;
using Domain.Entities.Responses.Clients;
using Domain.Entities.Responses.Masters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Superadmin")]
    public class AdminClinicsController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        private readonly ILogger<AdminClinicsController> _logger;
        private readonly HttpClient _httpClient;

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
                string authToken = HttpContext.Request.Headers["Authorization"];
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
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<UserDemo>>(APIType.Master, "Subscriptions/UserDemo", authToken);

                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("List")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetClinicList()
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<UserDataResponse>>(APIType.Master, "Auth/User/Entity", authToken);
                var dataList = new List<ClientClinicListResponse>();

                foreach (var item in response)
                {
                    try
                    {
                        var responseClinic = await _restAPIService.GetResponse<Clinics>(APIType.Client, "Data/ClinicsEntity/" + item.Entity, authToken);

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

                        var data = new ClientClinicListResponse();
                        var ownerData = new ClientOwnerResponse
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Email = item.Email,
                            IsVerified = item.IsVerified
                        };

                        var responseLatestBill = await _restAPIService.GetResponse<UserBillResponse>(APIType.Master, "BillPayments/Latest/" + item.Id, authToken);
                        string statusBill = "On Going";
                        if (responseLatestBill.EndDate < DateTime.Now || responseLatestBill.Status == "Expired")
                        {
                            statusBill = "Expired";
                        }
                        data.OwnerData = ownerData;
                        data.JoinDate = item.CreatedAt;
                        data.Id = item.Id;
                        data.ClinicData = clinicData;
                        data.Status = statusBill;
                        data.EndDate = responseLatestBill.EndDate;
                        dataList.Add(data);
                    }
                    catch
                    {
                        _logger.LogInformation("User Clinic of " + item.Email + " not found");
                    }

                }
                return ResponseUtil.CustomOk(dataList, 200, dataList.Count());
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Detail/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetClinicDetail(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
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
                string authToken = HttpContext.Request.Headers["Authorization"];
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
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<UserDataResponse>>(APIType.Master, "Auth/UserEntity/" + id, authToken);
                return ResponseUtil.CustomOk(response, 200);
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
        //        string authToken = HttpContext.Request.Headers["Authorization"];
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
        //        string authToken = HttpContext.Request.Headers["Authorization"];
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

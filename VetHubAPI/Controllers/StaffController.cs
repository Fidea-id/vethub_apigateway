using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses;
using Domain.Entities.Responses.Masters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StaffController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        public StaffController(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        [HttpGet]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetStaff([FromQuery] ProfileFilter filter)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<Profile>, ProfileFilter>(APIType.Client, "Profile", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetStaffById(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<Profile>(APIType.Client, $"Profile/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Authorize(Policy = "RequireOwnerRole")]
        public async Task<IActionResult> PostStaff([FromBody] ProfileRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var userId = User.FindFirstValue("Id");

                var responseClinic = await _restAPIService.GetResponse<Clinics>(APIType.Client, $"Data/Clinics", authToken);
                var responseProfile = await _restAPIService.GetResponse<DataResultDTO<Profile>>(APIType.Client, $"Profile", authToken);
                request.ClinicName = responseClinic.Name;
                var currentStaff = responseProfile.Data.Count();
                var responseBills = await _restAPIService.GetResponse<UserBillResponse>(APIType.Master, $"BillPayments/latest/{userId}", authToken);
                if (responseBills.MaxUser < (currentStaff + 1))
                    throw new Exception("Cannot add more than limit user clinic!");
                //register at master
                var response = await _restAPIService.PostResponse<RegisterResponse>(APIType.Master, "Auth/Register/Staff", JsonConvert.SerializeObject(request), authToken);
                //register at client
                var responseClient = await _restAPIService.PostResponse<RegisterResponse>(APIType.Client, $"Profile/{response.Id}", JsonConvert.SerializeObject(request), authToken);

                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("admin/{id}")]
        [Authorize(Policy = "RequireSuperadminRole")]
        public async Task<IActionResult> PostStaffAdmin(int id, [FromBody] ProfileRequest request)
        {
            try
            {
                //Get the AuthToken
                string? globalToken = HttpContext.Request.Headers["Authorization"];
                //get user entity
                var responseToken = await _restAPIService.GetResponse<UserDataResponse>(APIType.Master, "Auth/User/Entity/" + id, globalToken);
                var clientEntity = responseToken.Entity;
                var userId = id;

                var responseClinic = await _restAPIService.GetResponse<Clinics>(APIType.Client, $"Data/Clinics/admin/" + clientEntity, globalToken);
                var responseProfile = await _restAPIService.GetResponse<DataResultDTO<Profile>>(APIType.Client, $"Profile/admin/" + clientEntity, globalToken);
                request.ClinicName = responseClinic.Name;
                var currentStaff = responseProfile.Data.Count();
                var responseBills = await _restAPIService.GetResponse<UserBillResponse>(APIType.Master, $"BillPayments/latest/{userId}", globalToken);
                if (responseBills.MaxUser < (currentStaff + 1))
                    throw new Exception("Cannot add more than limit user clinic!");
                //register at master
                var response = await _restAPIService.PostResponse<RegisterResponse>(APIType.Master, "Auth/Register/Staff/" + clientEntity, JsonConvert.SerializeObject(request), globalToken);
                //register at client
                var responseClient = await _restAPIService.PostResponse<RegisterResponse>(APIType.Client, $"Profile/Admin/{clientEntity}/{response.Id}", JsonConvert.SerializeObject(request), globalToken);

                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("ActiveDeactive/{id}")]
        [Authorize(Policy = "RequireOwnerRole")]
        public async Task<IActionResult> ActiveDeactiveStaff(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.PutResponse<BaseAPIResponse>(APIType.Client, "Profile/Deactive", id, "", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "RequireOwnerRole")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] ProfileRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<Profile>(APIType.Client, "Profile", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireOwnerRole")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<Profile>(APIType.Client, $"Profile", id, authToken);

                if (response.GlobalId != 0)
                {
                    var responseMaster = await _restAPIService.DeleteResponse<UserProfileResponse>(APIType.Master, "Auth/Staff", response.GlobalId, authToken);
                }
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
    }
}

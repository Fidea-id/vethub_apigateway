using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses.Masters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<IEnumerable<Profile>, ProfileFilter>(APIType.Client, "Profile", authToken, filter);
                return ResponseUtil.CustomOkList<Profile, IEnumerable<Profile>>(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetStaffById(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<Profile>(APIType.Client, $"Profile/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostStaff([FromBody] ProfileRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];

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

        [HttpPost("ActiveDeactive/{id}")]
        public async Task<IActionResult> ActiveDeactiveStaff(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];

                return ResponseUtil.CustomOk("", 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] ProfileRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];

                return ResponseUtil.CustomOk("", 200);
            }
            catch
            {
                throw;
            }
        }
    }
}

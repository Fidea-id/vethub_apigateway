using Application.Services.Contracts;
using Application.Services.Implementations;
using Domain.Entities.Filters;
using Domain.Entities.Models.Clients;
using Domain.Entities;
using Domain.Entities.Models.Masters;
using Domain.Entities.Requests.Clients;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Application.Utils;
using Domain.Entities.Filters.Masters;
using Domain.Entities.DTOs;
using Domain.Entities.Responses.Masters;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        private readonly ILogger<SubscriptionController> _logger;
        private readonly HttpClient _httpClient;

        public SubscriptionController(IRestAPIService restAPIService,
            ILoggerFactory loggerFactory)
        {
            _restAPIService = restAPIService;
            _logger = loggerFactory.CreateLogger<SubscriptionController>();
        }

        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> Get([FromQuery] SubscriptionsFilter filter)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<IEnumerable<SubscriptionResponse>, SubscriptionsFilter>(APIType.Master, "Subscriptions", authToken, filter);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<SubscriptionResponse>(APIType.Master, $"Subscriptions/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Superadmin")]
        public async Task<IActionResult> Post([FromBody] Subscriptions request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<Subscriptions>(APIType.Master, "Subscriptions", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Superadmin")]
        public async Task<IActionResult> Put(int id, [FromBody] Subscriptions request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<Subscriptions>(APIType.Master, "Subscriptions", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Superadmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<Subscriptions>(APIType.Master, "Subscriptions", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
    }
}

using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Entities.Models.Clients;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationController : Controller
    {
        private readonly IRestAPIService _restAPIService;

        public NotificationController(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        [HttpGet("GetAllNotif")]
        public async Task<IActionResult> GetAllListNotif()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<Notifications>>(APIType.Client, "Notification/GetAllNotif", authToken);

                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        [HttpGet("GetRecentNotif")]
        public async Task<IActionResult> GetRecentNotif()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<Notifications>>(APIType.Client, "Notification/GetRecentNotif", authToken);

                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        [HttpPost("Read/{id:int}")]
        public async Task<IActionResult> ReadNotification(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.PostResponse<object>(APIType.Client, "Notification//Read/"+id, authToken);

                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("ReadAll")]
        public async Task<IActionResult> ReadAllNotifications()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.PostResponse<object>(APIType.Client, "Notification//ReadAll", authToken);

                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
    }
}

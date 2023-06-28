using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities.Models.Masters;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Requests.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequest data)
        {
            var result = await _authService.LoginAsync(data);
            return ResponseUtil.CustomOk(result, 200);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(FullRegisterClinicRequest data)
        {
            var result = await _authService.RegisterUserAsync(data);
            return ResponseUtil.CustomOk(result, 200);
        }

        [HttpGet("User")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUser()
        {
            //Get the AuthToken
            string authToken = HttpContext.Request.Headers["Authorization"];
            var userId = User.FindFirstValue("Id");
            var user = await _authService.GetUserProfileByIdAsync(int.Parse(userId), authToken);
            return ResponseUtil.CustomOk(user, 200);
        }

        //[HttpGet("GetEmailOrUserName")]
        //public async Task<IActionResult> GetUserEmail(string value)
        //{
        //    //Get the AuthToken
        //    string authToken = HttpContext.Request.Headers["Authorization"];
        //    var user = await _authService.GetByNameOrEmailAsync(value, authToken);
        //    return ResponseUtil.CustomOk(user, 200);
        //}

        [HttpPost("CheckUserActivation")]
        public async Task<IActionResult> CheckUserActivation(ActivationRequest request)
        {
            var check = await _authService.CheckUserActivationAsync(request);
            return Ok(check);
        }

        [HttpPost("UploadProfilePicture")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            //Get the AuthToken
            var user = User.FindFirstValue("Id");
            var check = await _authService.UploadProfilePicture(file, user);
            return Ok(check);
        }

        [HttpPut("UpdateProfile")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateProfile(ProfileRequest request)
        {
            //Get the AuthToken
            string authToken = HttpContext.Request.Headers["Authorization"];
            var userId = User.FindFirstValue("Id");
            var check = await _authService.UpdateProfile(int.Parse(userId), request, authToken);
            return Ok(check);
        }

        [HttpPost("UserActivation")]
        public async Task<IActionResult> UserActivation(ActivationRequest request)
        {
            var check = await _authService.UserActivationAsync(request);
            return Ok(check);
        }
    }
}
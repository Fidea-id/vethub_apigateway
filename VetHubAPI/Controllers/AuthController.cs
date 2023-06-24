﻿using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities.Models.Masters;
using Domain.Entities.Requests.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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

        [HttpGet("User/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUser(int id)
        {
            //Get the AuthToken
            string authToken = HttpContext.Request.Headers["Authorization"];
            var user = await _authService.GetUserProfileByIdAsync(id, authToken);
            return ResponseUtil.CustomOk(user, 200);
        }

        [HttpGet("GetEmailOrUserName")]
        public async Task<IActionResult> GetUserEmail(string value)
        {
            //Get the AuthToken
            string authToken = HttpContext.Request.Headers["Authorization"];
            var user = await _authService.GetByNameOrEmailAsync(value, authToken);
            return ResponseUtil.CustomOk(user, 200);
        }

        [HttpPost("CheckUserActivation")]
        public async Task<IActionResult> CheckUserActivation(ActivationRequest request)
        {
            var check = await _authService.CheckUserActivationAsync(request);
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
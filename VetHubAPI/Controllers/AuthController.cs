﻿using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Requests.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
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
        [HttpPost("Demo")]
        public async Task<IActionResult> Demo(UserDemoRequest data)
        {
            var result = await _authService.DemoAsync(data);
            return ResponseUtil.CustomOk(result, 200);
        }

        [HttpPost("Register")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Register(FullRegisterClinicRequest data)
        {
            //Get the AuthToken
            string? authToken = HttpContext.Request.Headers["Authorization"];
            var user = User;
            var result = await _authService.RegisterUserAsync(data, authToken);
            return ResponseUtil.CustomOk(result, 200);
        }


        [HttpGet("ResendEmailVerification/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ResendEmailVerification(int id)
        {
            //Get the AuthToken
            string? authToken = HttpContext.Request.Headers["Authorization"];
            var result = await _authService.ResendEmailVerif(id, authToken);
            return ResponseUtil.CustomOk(result, 200);
        }

        [HttpGet("User")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUser()
        {
            //Get the AuthToken
            string? authToken = HttpContext.Request.Headers["Authorization"];
            var userId = User.FindFirstValue("Id");
            var user = await _authService.GetUserProfileByIdAsync(int.Parse(userId), authToken);
            return ResponseUtil.CustomOk(user, 200);
        }

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
            var userId = User.FindFirstValue("Id");
            var check = await _authService.UploadProfilePicture(file, userId);
            return Ok(check);
        }

        [HttpPut("UpdateProfile")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateProfile(ProfileRequest request)
        {
            //Get the AuthToken
            string? authToken = HttpContext.Request.Headers["Authorization"];
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

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var check = await _authService.ForgotPasswordAsync(request);
            return Ok(check);
        }

        [HttpPost("ChangeForgotPassword")]
        public async Task<IActionResult> ChangeForgotPassword(ForgotPasswordRequest request)
        {
            var check = await _authService.ChangeForgotPasswordAsync(request);
            return Ok(check);
        }

        [HttpPost("ChangePassword")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ChangePassword(ForgotPasswordRequest request)
        {
            //Get the AuthToken
            string? authToken = HttpContext.Request.Headers["Authorization"];
            var userId = User.FindFirstValue("Id");
            var check = await _authService.ChangePasswordAsync(request, authToken);
            return Ok(check);
        }
    }
}
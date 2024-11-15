﻿using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Entities.Filters.Masters;
using Domain.Entities.Models.Masters;
using Domain.Entities.Requests.Masters;
using Domain.Entities.Responses;
using Domain.Entities.Responses.Masters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequireSuperadminRole")]
    public class BillsController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        public BillsController(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        [HttpGet("UpdateBills/{userId}")]
        public async Task<IActionResult> UpdateBills(int userId)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<UserBillResponse>(APIType.Master, "BillPayments/UpdateBills/" + userId, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("UpdateUserSubs")]
        public async Task<IActionResult> UpdateUserSubs([FromBody] UserSubsRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<UserBillResponse>(APIType.Master, "BillPayments/UpdateUserSubs", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("StopBills/{userId}")]
        public async Task<IActionResult> StopBills(int userId)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<UserBillResponse>(APIType.Master, "BillPayments/StopBills/" + userId, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("DeleteClinics/{userId}")]
        public async Task<IActionResult> DeleteClinics(int userId)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<BaseAPIResponse>(APIType.Master, "BillPayments/DeleteClinics/" + userId, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetBillPayment([FromQuery] BillPaymentsFilter filter)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<BillPayments>, BillPaymentsFilter>(APIType.Master, "BillPayments", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetBillPaymentById(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<BillPayments>(APIType.Master, $"BillPayments/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("status")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetBillStatus()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<BillPayments>, BillPaymentsFilter>(APIType.Master, "BillPayments/status", authToken);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("status/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetBillStatustById(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<BillPayments>(APIType.Master, $"BillPayments/status/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Owners")]
        public async Task<IActionResult> PostBillPayments([FromBody] BillPayments request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<BillPayments>(APIType.Master, "BillPayments", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
    }
}

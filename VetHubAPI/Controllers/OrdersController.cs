﻿using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses;
using Domain.Entities.Responses.Clients;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        public OrdersController(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        [HttpGet("Dashboard")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetOrderDashboard()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<DashboardOrderResponse>(APIType.Client, "Orders/Dashboard", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Full")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetOrderFull()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<OrderFullResponse>>(APIType.Client, "Orders/Full", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Full/Month")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetOrderFullMonth()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<OrderFullResponse>>(APIType.Client, "Orders/FullMonth", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Full")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> PostOrderFull([FromBody] OrderFullRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<OrderFullResponse>(APIType.Client, "Orders/Full", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Full/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetOrderFullById(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<OrderFullResponse>(APIType.Client, $"Orders/Full/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderById(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<BaseAPIResponse>(APIType.Client, $"Orders", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Payment")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> PostOrderPayment([FromBody] OrdersPaymentRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<OrdersPaymentResponse>(APIType.Client, "Orders/Payment", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
    }
}

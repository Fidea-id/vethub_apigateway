using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.Filters.Masters;
using Domain.Entities.Models.Masters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BillsController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        public BillsController(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        [HttpGet]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetBillPayment([FromQuery] BillPaymentsFilter filter)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<IEnumerable<BillPayments>, BillPaymentsFilter>(APIType.Master, "BillPayments", authToken, filter);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetBillPaymentById(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<BillPayments>(APIType.Master, $"BillPayments/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("status")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetBillStatus()
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<IEnumerable<BillPayments>, BillPaymentsFilter>(APIType.Master, "BillPayments/status", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("status/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetBillStatustById(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<BillPayments>(APIType.Master, $"BillPayments/status/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Owners")]
        public async Task<IActionResult> PostBillPayments([FromBody] BillPayments request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<BillPayments>(APIType.Master, "BillPayments", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

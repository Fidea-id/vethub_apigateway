using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Requests.Clients;
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
    public class FinancialController : Controller
    {
        private readonly IRestAPIService _restAPIService;

        public FinancialController(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        [HttpPost("Transaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] FinancialTransactionsRequest request)
        {
            try
            {
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<FinancialTransactionsResponse>(APIType.Client, "Financial/Transaction", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Journal")]
        public async Task<IActionResult> GetJournal([FromQuery] FinancialTransactionsFilter filter)
        {
            try
            {
                string? authToken = HttpContext.Request.Headers["Authorization"];

                // Build query string
                var queryParams = new List<string>();
                if (filter.StartDate.HasValue) queryParams.Add($"StartDate={filter.StartDate.Value:O}");
                if (filter.EndDate.HasValue) queryParams.Add($"EndDate={filter.EndDate.Value:O}");

                var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
                var url = $"Financial/Journal{queryString}";

                var response = await _restAPIService.GetResponse<IEnumerable<FinancialTransactionsResponse>>(APIType.Client, url, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("BalanceSheet")]
        public async Task<IActionResult> GetBalanceSheet([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var queryParams = new List<string>();
                if (startDate.HasValue) queryParams.Add($"startDate={startDate.Value:O}");
                if (endDate.HasValue) queryParams.Add($"endDate={endDate.Value:O}");
                var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
                var url = $"Financial/BalanceSheet{queryString}";
                var response = await _restAPIService.GetResponse<FinancialReportResponse>(APIType.Client, url, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("ProfitLoss")]
        public async Task<IActionResult> GetProfitLoss([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var queryParams = new List<string>();
                if (startDate.HasValue) queryParams.Add($"startDate={startDate.Value:O}");
                if (endDate.HasValue) queryParams.Add($"endDate={endDate.Value:O}");
                var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";

                var response = await _restAPIService.GetResponse<FinancialReportResponse>(APIType.Client, $"Financial/ProfitLoss{queryString}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("SyncHistorical")]
        public async Task<IActionResult> SyncHistorical()
        {
            try
            {
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.PostResponse<object>(APIType.Client, "Financial/SyncHistorical", "", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Expense")]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseRequest request)
        {
            try
            {
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<object>(APIType.Client, "Financial/Expense", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
    }
}

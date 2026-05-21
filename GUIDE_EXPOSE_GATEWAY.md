# Guide: Exposing Financial Endpoints in API Gateway

This guide explains how to expose the newly created financial endpoints from the `ClientVetHub` service through the `VetHubAPI` Gateway.

## Prerequisites

Ensure you have implemented the endpoints in `ClientVetHub`'s `FinancialController`.

## Steps to Expose Endpoints

### 1. Create Gateway Controller
Create a new file `FinancialController.cs` in the `VetHubAPI/Controllers` directory.

### 2. Implementation Template
The Gateway uses `IRestAPIService` to proxy requests. Use the following template for the implementation:

```csharp
using Application.Services.Contracts;
using Application.Utils;
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
            string? authToken = HttpContext.Request.Headers["Authorization"];
            var requestJson = JsonConvert.SerializeObject(request);
            var response = await _restAPIService.PostResponse<FinancialTransactionsResponse>(APIType.Client, "Financial/Transaction", requestJson, authToken);
            return ResponseUtil.CustomOk(response, 200);
        }

        [HttpGet("Journal")]
        public async Task<IActionResult> GetJournal([FromQuery] FinancialTransactionsFilter filter)
        {
            string? authToken = HttpContext.Request.Headers["Authorization"];
            
            // Build query string
            var queryParams = new List<string>();
            if (filter.StartDate.HasValue) queryParams.Add($"StartDate={filter.StartDate.Value:O}");
            if (filter.EndDate.HasValue) queryParams.Add($"EndDate={filter.EndDate.Value:O}");
            if (filter.AccountId.HasValue) queryParams.Add($"AccountId={filter.AccountId}");
            
            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
            var url = $"Financial/Journal{queryString}";

            var response = await _restAPIService.GetResponse<IEnumerable<FinancialTransactionsResponse>>(APIType.Client, url, authToken);
            return ResponseUtil.CustomOk(response, 200);
        }

        [HttpGet("BalanceSheet")]
        public async Task<IActionResult> GetBalanceSheet([FromQuery] DateTime? date)
        {
            string? authToken = HttpContext.Request.Headers["Authorization"];
            var url = $"Financial/BalanceSheet" + (date.HasValue ? $"?date={date.Value:O}" : "");
            var response = await _restAPIService.GetResponse<FinancialReportResponse>(APIType.Client, url, authToken);
            return ResponseUtil.CustomOk(response, 200);
        }

        [HttpGet("ProfitLoss")]
        public async Task<IActionResult> GetProfitLoss([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            string? authToken = HttpContext.Request.Headers["Authorization"];
            var queryParams = new List<string>();
            if (startDate.HasValue) queryParams.Add($"startDate={startDate.Value:O}");
            if (endDate.HasValue) queryParams.Add($"endDate={endDate.Value:O}");
            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
            
            var response = await _restAPIService.GetResponse<FinancialReportResponse>(APIType.Client, $"Financial/ProfitLoss{queryString}", authToken);
            return ResponseUtil.CustomOk(response, 200);
        }

        [HttpPost("SyncHistorical")]
        public async Task<IActionResult> SyncHistorical()
        {
            string? authToken = HttpContext.Request.Headers["Authorization"];
            var response = await _restAPIService.PostResponse<object>(APIType.Client, "Financial/SyncHistorical", "", authToken);
            return ResponseUtil.CustomOk(response, 200);
        }

        [HttpPost("Expense")]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseRequest request)
        {
            string? authToken = HttpContext.Request.Headers["Authorization"];
            var requestJson = JsonConvert.SerializeObject(request);
            var response = await _restAPIService.PostResponse<object>(APIType.Client, "Financial/Expense", requestJson, authToken);
            return ResponseUtil.CustomOk(response, 200);
        }
    }
}
```

### 3. Key Considerations
- **APIType**: Always use `APIType.Client` for requests targeting the `ClientVetHub` service.
- **AuthToken**: Pass the `Authorization` header from the current request to ensure the downstream service can identify the user/tenant.
- **Serialization**: Use `JsonConvert.SerializeObject` for POST/PUT request bodies.
- **Query Parameters**: Manually construct the query string for GET requests as shown in the examples.

## Testing
Once implemented, you can call the gateway at `https://<gateway-url>/api/Financial/...` and it will correctly route the request to the underlying client service.

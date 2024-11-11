using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
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
    public class OpnamesController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        public OpnamesController(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        #region Opnames
        [HttpGet]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetOpnames([FromQuery] OpnamesFilter filter)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<Opnames>, OpnamesFilter>(APIType.Client, "Opname", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetOpnamesById(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<Opnames>(APIType.Client, $"Opname/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostOpnames([FromBody] OpnamesRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<Opnames>(APIType.Client, "Opname", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOpnames(int id, [FromBody] OpnamesRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<Opnames>(APIType.Client, "Opname", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpnames(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<Opnames>(APIType.Client, "Opname", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region OpnamePatients
        [HttpGet("Patients/Close/{medId}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> PostCloseOpname(int medId)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<OpnamePatients>(APIType.Client, $"MedicalRecords/CloseOpname/{medId}", authToken);

                return ResponseUtil.CustomOk(response, 200, 1);
            }
            catch
            {
                throw;
            }
        }
        [HttpGet("Patients/Detail/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetOpnamePatientsDetailById(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<OpnamePatients>(APIType.Client, $"Opname/OpnamePatients/{id}", authToken);

                var opnameResponse = await _restAPIService.GetResponse<Opnames>(APIType.Client, $"Opname/{response.OpnameId}", authToken);
                var medicalResponse = await _restAPIService.GetResponse<MedicalRecordsDetailResponse>(APIType.Client, $"MedicalRecords/Detail/{response.MedicalRecordId}", authToken);

                var result = new OpnamePatientsDetailFullResponse();
                var itemResult = new OpnamePatientsDetailResponse();
                itemResult.Id = response.Id;
                itemResult.OpnameId = response.OpnameId;
                itemResult.MedicalRecordId = response.MedicalRecordId;
                itemResult.Status = response.Status;
                itemResult.StartTime = response.StartTime;
                itemResult.EndTime = response.EndTime;
                itemResult.EstimatedDays = response.EstimateDays;
                itemResult.Price = response.Price;
                itemResult.TotalPrice = response.TotalPrice;
                itemResult.OpnameName = opnameResponse.Name;
                itemResult.PatientName = medicalResponse.Patients.Name;
                itemResult.PatientId = medicalResponse.Patients.Id;

                result.OpnameDetail = itemResult;
                result.MedicalDetail = medicalResponse;

                return ResponseUtil.CustomOk(result, 200, 1);
            }
            catch
            {
                throw;
            }
        }
        [HttpGet("Patients/Detail")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetOpnamePatientsDetail([FromQuery] OpnamePatientsFilter filter)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<DataResultDTO<OpnamePatientsDetailResponse>>(APIType.Client, "Opname/OpnamePatients/Detail", authToken);

                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }
        [HttpGet("Patients")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetOpnamePatients([FromQuery] OpnamePatientsFilter filter)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<OpnamePatients>, OpnamePatientsFilter>(APIType.Client, "Opname/OpnamePatients", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Patients/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetOpnamePatientsById(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<OpnamePatients>(APIType.Client, $"Opname/OpnamePatients/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Patients")]
        public async Task<IActionResult> PostOpnamePatients([FromBody] OpnamePatientsRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<OpnamePatients>(APIType.Client, "Opname/OpnamePatients", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("Patients/{id}")]
        public async Task<IActionResult> PutOpnamePatients(int id, [FromBody] OpnamePatientsRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<OpnamePatients>(APIType.Client, "Opname/OpnamePatients", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("Patients/{id}")]
        public async Task<IActionResult> DeleteOpnamePatients(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<OpnamePatients>(APIType.Client, "Opname/OpnamePatients", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}

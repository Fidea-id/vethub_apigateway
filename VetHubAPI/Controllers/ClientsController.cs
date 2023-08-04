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
using System.Security.Claims;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientsController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        private readonly IFileUploadService _fileUploadService;
        public ClientsController(IRestAPIService restAPIService, IFileUploadService fileUploadService)
        {
            _restAPIService = restAPIService;
            _fileUploadService = fileUploadService;
        }

        //Clients Owner
        [HttpGet("Owners")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetOwner([FromQuery] OwnersFilter filter)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<Owners>, OwnersFilter>(APIType.Client, "Owners", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Owners/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetOwnerById(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<Owners>(APIType.Client, $"Owners/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Owners")]
        public async Task<IActionResult> PostOwner([FromBody] OwnersPetsRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<Owners>(APIType.Client, "Owners", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Owners/Image")]
        public async Task<IActionResult> UploadOwnersPicture(IFormFile file)
        {
            //Get the AuthToken
            var user = User.FindFirstValue("Id");
            var upload = await _fileUploadService.UploadImageAsync(file, $"owners/{user}");
            return Ok(upload);
        }


        [HttpPut("Owners/{id}")]
        public async Task<IActionResult> PutOwner(int id, [FromBody] OwnersRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<Owners>(APIType.Client, "Owners", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("Owners/{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<Owners>(APIType.Client, "Owners", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        //Clients Owner History

        //Clients Patients
        [HttpGet("Patients")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetPatient([FromQuery] PatientsFilter filter)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<Patients>, PatientsFilter>(APIType.Client, "Patients", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Patients/List")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetPatientList([FromQuery] PatientsFilter filter)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<PatientsListResponse>, PatientsFilter>(APIType.Client, "Patients/List", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Patients/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetPatientById(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<Patients>(APIType.Client, $"Patients/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Patients/Owner/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetPatientByOwnerId(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<Patients>>(APIType.Client, $"Patients/Owner/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200, response.Count());
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Patients")]
        public async Task<IActionResult> PostPatient([FromBody] Patients request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<Patients>(APIType.Client, "Patients", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Patients/Image")]
        public async Task<IActionResult> UploadPatientsPicture(IFormFile file)
        {
            //Get the AuthToken
            var user = User.FindFirstValue("Id");
            var upload = await _fileUploadService.UploadImageAsync(file, $"patients/{user}");
            return Ok(upload);
        }


        [HttpPut("Patients/{id}")]
        public async Task<IActionResult> PutPatient(int id, [FromBody] PatientsRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<Patients>(APIType.Client, "Patients", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("Patients/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<Patients>(APIType.Client, "Patients", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        //Clients Patients Statistic
        [HttpGet("Patients/Statistic/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetPatientLatestStatisticId(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<PatientsStatisticResponse>>(APIType.Client, $"Patients/Statistic/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200, response.Count());
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Patients/Statistic")]
        public async Task<IActionResult> PostPatientStatistic([FromBody] PatientsStatisticRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<PatientsStatistic>(APIType.Client, "Patients/Statistic", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        //Clients Patients History
    }
}

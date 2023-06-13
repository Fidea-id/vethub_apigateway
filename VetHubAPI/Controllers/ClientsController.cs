using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientsController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        public ClientsController(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
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
                var response = await _restAPIService.GetResponseFilter<IEnumerable<Owners>, OwnersFilter>(APIType.Client, "Owners", authToken, filter);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Owners")]
        public async Task<IActionResult> PostOwner([FromBody] Owners request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<Owners>(APIType.Client, "Owners", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
                var response = await _restAPIService.GetResponseFilter<IEnumerable<Patients>, PatientsFilter>(APIType.Client, "Patients", authToken, filter);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Clients Patients History
    }
}

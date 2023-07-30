using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Application.Utils;
using Application.Services.Contracts;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Filters;
using Domain.Entities.Responses.Clients;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DataController : Controller
    {
        private readonly IRestAPIService _restAPIService;

        public DataController(IRestAPIService restAPIAnimal)
        {
            _restAPIService = restAPIAnimal;
        }

        #region Animal
        [HttpGet("Animal")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetAnimal([FromQuery] NameBaseEntityFilter filter)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<IEnumerable<Animals>, NameBaseEntityFilter>(APIType.Client, "Data/Animal", authToken, filter);
                return ResponseUtil.CustomOkList<Animals, IEnumerable<Animals>>(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Animal/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetAnimalById(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<Animals>(APIType.Client, $"Data/Animal/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Animal")]
        public async Task<IActionResult> PostAnimal([FromBody] AnimalsRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<Animals>(APIType.Client, "Data/Animal", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Animal/{id}")]
        public async Task<IActionResult> PutAnimal(int id, [FromBody] AnimalsRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<Animals>(APIType.Client, "Data/Animal", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Animal/{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<Animals>(APIType.Client, "Data/Animal", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Breed
        [HttpGet("Breed")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetBreed([FromQuery] NameBaseEntityFilter filter)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<IEnumerable<BreedAnimalResponse>, NameBaseEntityFilter>(APIType.Client, "Data/Breed", authToken, filter);
                return ResponseUtil.CustomOkList<BreedAnimalResponse, IEnumerable<BreedAnimalResponse>>(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Breed/{idAnimal}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetBreedById(int idAnimal)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<Breeds>(APIType.Client, $"Data/Breed/{idAnimal}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Breed")]
        public async Task<IActionResult> PostBreed([FromBody] BreedsRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<Breeds>(APIType.Client, "Data/Breed", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Breed/{id}")]
        public async Task<IActionResult> PutBreed(int id, [FromBody] BreedsRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<Breeds>(APIType.Client, "Data/Breed", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Breed{id}")]
        public async Task<IActionResult> DeleteBreed(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<Breeds>(APIType.Client, "Data/Breed", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion
    }
}

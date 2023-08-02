using Application.Services.Contracts;
using Domain.Entities.Responses;
using Microsoft.AspNetCore.Mvc;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        public PlacesController(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        [HttpGet("Province")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> Province()
        {
            try
            {
                var response = await _restAPIService.GetPlaceResponse<GoAPIResponse>("provinsi");
                return Ok(response);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("City/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> City(string id)
        {
            try
            {
                var response = await _restAPIService.GetPlaceResponse<GoAPIResponse>("kota", $"provinsi_id={id}");
                return Ok(response);
            }
            catch
            {
                throw;
            }
        }
    }
}

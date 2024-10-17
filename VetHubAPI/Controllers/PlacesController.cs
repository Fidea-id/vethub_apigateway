using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.Models.Masters;
using Domain.Entities.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
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
                //var response = await _restAPIService.GetPlaceResponse<GoAPIResponse>("provinsi");
                var response = await _restAPIService.GetResponse<IEnumerable<Provinces>>(APIType.Master, "PublicData/Provinces");
                return ResponseUtil.CustomOk(response, 200);
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
                //var response = await _restAPIService.GetPlaceResponse<GoAPIResponse>("kota", $"provinsi_id={id}");
                var response = await _restAPIService.GetResponse<IEnumerable<States>>(APIType.Master, "PublicData/States/" + id);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
    }
}

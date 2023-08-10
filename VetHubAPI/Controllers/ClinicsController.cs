using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Masters;
using Domain.Entities.Responses.Masters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Superadmin")]
    public class ClinicsController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        public ClinicsController(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        //[HttpGet("List")]
        //[ResponseCache(Duration = 60)] // Cache response for 60 seconds
        //public async Task<IActionResult> GetClinicList()
        //{
        //    try
        //    {
        //        //Get the AuthToken
        //        string authToken = HttpContext.Request.Headers["Authorization"];
        //        var response = await _restAPIService.GetResponseFilter<DataResultDTO<ClientClinicListResponse>, ClinicsFilter>(APIType.Master, "Clinics/List", authToken);
        //        return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //[HttpGet("Detail/{id}")]
        //[ResponseCache(Duration = 60)] // Cache response for 60 seconds
        //public async Task<IActionResult> GetClinicDetail(int id)
        //{
        //    try
        //    {
        //        //Get the AuthToken
        //        string authToken = HttpContext.Request.Headers["Authorization"];
        //        var response = await _restAPIService.GetResponseFilter<ClientClinicDetailResponse, ClinicsFilter>(APIType.Master, $"Clinics/detail/{id}", authToken);
        //        return ResponseUtil.CustomOk(response, 200);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //[HttpGet]
        //[ResponseCache(Duration = 60)] // Cache response for 60 seconds
        //public async Task<IActionResult> GetClinic([FromQuery] ClinicsFilter filter)
        //{
        //    try
        //    {
        //        //Get the AuthToken
        //        string authToken = HttpContext.Request.Headers["Authorization"];
        //        var response = await _restAPIService.GetResponseFilter<DataResultDTO<ClinicsEntity>, ClinicsFilter>(APIType.Master, "Clinics", authToken, filter);
        //        return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //[HttpGet("{id}")]
        //[ResponseCache(Duration = 60)] // Cache response for 60 seconds
        //public async Task<IActionResult> GetClinicById(int id)
        //{
        //    try
        //    {
        //        //Get the AuthToken
        //        string authToken = HttpContext.Request.Headers["Authorization"];
        //        var response = await _restAPIService.GetResponse<ClinicsEntity>(APIType.Master, $"Clinics/{id}", authToken);
        //        return ResponseUtil.CustomOk(response, 200);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}

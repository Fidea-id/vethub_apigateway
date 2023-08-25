using Application.Services.Contracts;
using Domain.Entities;
using Domain.Entities.DTOs.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Models.Masters;
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
    public class GenerateController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        private readonly IDocGenerateService _generateService; 

        public GenerateController(IRestAPIService restAPIService, IDocGenerateService generateService)
        {
            _restAPIService = restAPIService;
            _generateService = generateService;
        }

        [HttpPost("SuratKematian")]
        public async Task<IActionResult> GenerateSuratKematian(DocsKematianRequest request)
        {
            //Get the AuthToken
            string authToken = HttpContext.Request.Headers["Authorization"];
            var userId = User.FindFirstValue("Id");
            var generate = await _generateService.GenerateSuratKematianAsync(userId, request, authToken);
            // post medicalrecordsnote data
            var noteRequest = new MedicalRecordsNotesRequest
            {
                MedicalRecordsId = request.MedicalRecordsId,
                Title = generate.Filename,
                Type = generate.Type,
                Value = generate.Url
            };
            var postNote = await _restAPIService.PostResponse<MedicalRecordsNotesResponse>(APIType.Client, "MedicalRecords/Notes", JsonConvert.SerializeObject(noteRequest), authToken);

            return Ok(postNote);
        }
    }
}

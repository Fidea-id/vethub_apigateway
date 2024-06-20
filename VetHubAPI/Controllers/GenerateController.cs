using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
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
        private readonly ILogger<GenerateController> _logger;

        public GenerateController(IRestAPIService restAPIService, IDocGenerateService generateService,
            ILoggerFactory loggerFactory)
        {
            _restAPIService = restAPIService;
            _generateService = generateService;
            _logger = loggerFactory.CreateLogger<GenerateController>();
        }

        [HttpPost("SuratKematian")]
        public async Task<IActionResult> GenerateSuratKematian(DocsKematianRequest request)
        {
            //Get the AuthToken
            string? authToken = HttpContext.Request.Headers["Authorization"];
            var userId = User.FindFirstValue("Id");
            var generate = await _generateService.GenerateSuratKematianAsync(userId, request, authToken);
            _logger.LogInformation("Generate data : " + JsonConvert.SerializeObject(generate));
            // post medicalrecordsnote data
            var noteRequest = new MedicalRecordsNotesRequest
            {
                MedicalRecordsId = request.MedicalRecordsId,
                Title = generate.Filename,
                Type = generate.Type,
                Value = generate.Url
            };
            var postNote = await _restAPIService.PostResponse<MedicalRecordsNotesResponse>(APIType.Client, "MedicalRecords/Notes", JsonConvert.SerializeObject(noteRequest), authToken);

            return ResponseUtil.CustomOk(postNote, 200);
        }

        [HttpPost("SuratPermintaanPulang")]
        public async Task<IActionResult> GenerateSuratPermintaanPulang(DocsPermintaanPulangRequest request)
        {
            //Get the AuthToken
            string? authToken = HttpContext.Request.Headers["Authorization"];
            var userId = User.FindFirstValue("Id");
            var generate = await _generateService.GenerateSuratPermintaanPulangAsync(userId, request, authToken);

            _logger.LogInformation("Generate data : " + JsonConvert.SerializeObject(generate));
            // post medicalrecordsnote data
            var noteRequest = new MedicalRecordsNotesRequest
            {
                MedicalRecordsId = request.MedicalRecordsId,
                Title = generate.Filename,
                Type = generate.Type,
                Value = generate.Url
            };
            var postNote = await _restAPIService.PostResponse<MedicalRecordsNotesResponse>(APIType.Client, "MedicalRecords/Notes", JsonConvert.SerializeObject(noteRequest), authToken);

            return ResponseUtil.CustomOk(postNote, 200);
        }

        [HttpPost("SuratTindakan")]
        public async Task<IActionResult> GenerateSuratTindakan(DocsTindakanRequest request)
        {
            //Get the AuthToken
            string? authToken = HttpContext.Request.Headers["Authorization"];
            var userId = User.FindFirstValue("Id");
            var generate = await _generateService.GenerateSuratTindakanAsync(userId, request, authToken);
            _logger.LogInformation("Generate data : " + JsonConvert.SerializeObject(generate));
            // post medicalrecordsnote data
            var noteRequest = new MedicalRecordsNotesRequest
            {
                MedicalRecordsId = request.MedicalRecordsId,
                Title = generate.Filename,
                Type = generate.Type,
                Value = generate.Url
            };
            var postNote = await _restAPIService.PostResponse<MedicalRecordsNotesResponse>(APIType.Client, "MedicalRecords/Notes", JsonConvert.SerializeObject(noteRequest), authToken);

            return ResponseUtil.CustomOk(postNote, 200);
        }

        [HttpPost("SuratRujukan")]
        public async Task<IActionResult> GenerateSuratRujukan(DocsRujukanRequest request)
        {
            //Get the AuthToken
            string? authToken = HttpContext.Request.Headers["Authorization"];
            var userId = User.FindFirstValue("Id");
            var generate = await _generateService.GenerateSuratRujukanAsync(userId, request, authToken);
            _logger.LogInformation("Generate data : " + JsonConvert.SerializeObject(generate));
            // post medicalrecordsnote data
            var noteRequest = new MedicalRecordsNotesRequest
            {
                MedicalRecordsId = request.MedicalRecordsId,
                Title = generate.Filename,
                Type = generate.Type,
                Value = generate.Url
            };
            var postNote = await _restAPIService.PostResponse<MedicalRecordsNotesResponse>(APIType.Client, "MedicalRecords/Notes", JsonConvert.SerializeObject(noteRequest), authToken);

            return ResponseUtil.CustomOk(postNote, 200);
        }
    }
}

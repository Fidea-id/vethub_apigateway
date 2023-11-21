using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Entities.DTOs.Clients;
using Domain.Entities.Filters;
using Domain.Entities.Models.Clients;
using Domain.Entities.Models.Masters;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses;
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
    public class DataController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        private readonly IFileUploadService _fileUploadService;

        public DataController(IRestAPIService restAPIAnimal, IFileUploadService fileUploadService)
        {
            _restAPIService = restAPIAnimal;
            _fileUploadService = fileUploadService;
        }

        [HttpGet("PrescriptionItem")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetPrescriptionItem()
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var responseProduct = await _restAPIService.GetResponse<IEnumerable<ProductDetailsResponse>>(APIType.Client, "Products/Detail", authToken);
                var responseService = await _restAPIService.GetResponse<DataResultDTO<Services>>(APIType.Client, "Services", authToken);
                var response = new List<PrescriptionsItemDTO>();
                foreach (var item in responseProduct)
                {
                    var data = new PrescriptionsItemDTO
                    {
                        Id = item.Id,
                        Type = "Product",
                        Name = item.Name,
                        Price = item.Price,
                        Description = item.Description,
                        Stock = item.Stock,
                        Volume = item.Volume
                    };
                    response.Add(data);
                }
                foreach (var item in responseService.Data)
                {
                    var data = new PrescriptionsItemDTO
                    {
                        Id = item.Id,
                        Type = "Service",
                        Name = item.Name,
                        Price = item.Price,
                        Description = item.Duration + " " + item.DurationType,
                        Stock = 0,
                        Volume = "service"
                    };
                    response.Add(data);
                }
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        [HttpGet("DashboardData")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetDashboardData()
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<DashboardResponse>(APIType.Client, "Data/DashboardData", authToken);
                
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        [HttpGet("DashboardDataAdmin")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetDashboardDataAdmin()
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<DashboardAdminResponse>(APIType.Master, "Data/DashboardData", authToken);
                
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("DocsType")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetDocsType()
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<DocsType>(APIType.Master, "PublicData/DocsType", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        #region Clinics
        [HttpGet("Clinics")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetClinics([FromQuery] NameBaseEntityFilter filter)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<Clinics>(APIType.Client, "Data/Clinics", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Clinics")]
        public async Task<IActionResult> PostClinics([FromBody] ClinicsRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<Clinics>(APIType.Client, "Data/Clinics", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Clinics/UploadLogo")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UploadClinicLogo(IFormFile file)
        {
            //Get the AuthToken
            var userId = User.FindFirstValue("Id");
            var upload = await _fileUploadService.UploadImageAsync(file, $"clg/{userId}");
            return Ok(upload);
        }

        [HttpPut("Clinics/{id}")]
        public async Task<IActionResult> PutClinics(int id, [FromBody] ClinicsRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<Clinics>(APIType.Client, "Data/Clinics", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Animal
        [HttpGet("Animal")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetAnimal([FromQuery] NameBaseEntityFilter filter)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<Animals>, NameBaseEntityFilter>(APIType.Client, "Data/Animal", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
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
            catch
            {
                throw;
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
            catch
            {
                throw;
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
            catch
            {
                throw;
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
            catch
            {
                throw;
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
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<BreedAnimalResponse>, NameBaseEntityFilter>(APIType.Client, "Data/Breed", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
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
            catch
            {
                throw;
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
            catch
            {
                throw;
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
            catch
            {
                throw;
            }
        }

        [HttpDelete("Breed/{id}")]
        public async Task<IActionResult> DeleteBreed(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<Breeds>(APIType.Client, "Data/Breed", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Diagnose
        [HttpGet("Diagnose")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetDiagnose([FromQuery] NameBaseEntityFilter filter)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<Diagnoses>, NameBaseEntityFilter>(APIType.Client, "Data/Diagnose", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Diagnose/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetDiagnoseById(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<Diagnoses>(APIType.Client, $"Data/Diagnose/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Diagnose")]
        public async Task<IActionResult> PostDiagnose([FromBody] DiagnosesRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<Diagnoses>(APIType.Client, "Data/Diagnose", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("Diagnose/{id}")]
        public async Task<IActionResult> PutDiagnose(int id, [FromBody] DiagnosesRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<Diagnoses>(APIType.Client, "Data/Diagnose", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("Diagnose/{id}")]
        public async Task<IActionResult> DeleteDiagnose(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<Diagnoses>(APIType.Client, "Data/Diagnose", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region PaymentMethod
        [HttpGet("PaymentMethod")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetPaymentMethod([FromQuery] NameBaseEntityFilter filter)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<PaymentMethod>, NameBaseEntityFilter>(APIType.Client, "Data/PaymentMethod", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("PaymentMethod/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetPaymentMethodById(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<PaymentMethod>(APIType.Client, $"Data/PaymentMethod/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("PaymentMethod")]
        public async Task<IActionResult> PostPaymentMethod([FromBody] PaymentMethodRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<PaymentMethod>(APIType.Client, "Data/PaymentMethod", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("PaymentMethod/{id}")]
        public async Task<IActionResult> PutPaymentMethod(int id, [FromBody] PaymentMethodRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<PaymentMethod>(APIType.Client, "Data/PaymentMethod", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("PaymentMethod/{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<PaymentMethod>(APIType.Client, "Data/PaymentMethod", id, authToken);
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

using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses;
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
    public class ProductsController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        public ProductsController(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        [HttpPost("Full")]
        public async Task<IActionResult> PostProductFull([FromBody] ProductAsBundleRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                if (request.IsBundle)
                {
                    var response = await _restAPIService.PostResponse<Products>(APIType.Client, "Products/Bundle", requestJson, authToken);
                    return ResponseUtil.CustomOk(response, 200);
                }
                else
                {
                    var response = await _restAPIService.PostResponse<Products>(APIType.Client, "Products", requestJson, authToken);
                    return ResponseUtil.CustomOk(response, 200);
                }
            }
            catch
            {
                throw;
            }
        }
        #region Product
        [HttpGet]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetProduct([FromQuery] ProductsFilter filter)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<Products>, ProductsFilter>(APIType.Client, "Products", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<Products>(APIType.Client, $"Products/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Products request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<Products>(APIType.Client, "Products", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("Deactive/{id}")]
        public async Task<IActionResult> DeactiveProduct(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.PutResponse<BaseAPIResponse>(APIType.Client, "Products/Deactive", id, "", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromBody] ProductsRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<Products>(APIType.Client, "Products", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<Products>(APIType.Client, "Products", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ProductBundle
        [HttpPost("Bundle")]
        public async Task<IActionResult> PostProductAsBundle([FromBody] ProductAsBundleRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<Products>(APIType.Client, "Products/Bundle", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Product Category
        [HttpGet("Category")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetProductCategory([FromQuery] ProductCategoriesFilter filter)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<ProductCategories>, ProductCategoriesFilter>(APIType.Client, "Products/Category", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Category/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetProductCategoryById(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<ProductCategories>(APIType.Client, $"Products/Category/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Category")]
        public async Task<IActionResult> PostProductCategory([FromBody] ProductsCategoriesRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<ProductCategories>(APIType.Client, "Products/Category", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("Category/{id}")]
        public async Task<IActionResult> PutProductCategory(int id, [FromBody] ProductsCategoriesRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<ProductCategories>(APIType.Client, "Products/Category", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("Category/{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<ProductCategories>(APIType.Client, "Products/Category", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ProductDetail
        [HttpGet("Detail")]
        public async Task<IActionResult> GetProductDetail()
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<ProductDetailsResponse>>(APIType.Client, "Products/Detail", authToken);
                return ResponseUtil.CustomOk(response, 200, response.Count());
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> GetProductDetail(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"]!;
                var response = await _restAPIService.GetResponse<ProductDetailsResponse>(APIType.Client, "Products/Detail/" + id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ProductDiscount
        [HttpGet("Discount")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetProductDiscount([FromQuery] ProductDiscountsFilter filter)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<ProductDiscountDetailResponse>, ProductDiscountsFilter>(APIType.Client, "Products/Discount", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Discount/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetProductDiscountById(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<ProductDiscounts>(APIType.Client, $"Products/Discount/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Discount")]
        public async Task<IActionResult> PostProductDiscount([FromBody] ProductsDiscountsRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<ProductDiscounts>(APIType.Client, "Products/Discount", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("Discount/{id}")]
        public async Task<IActionResult> PutProductDiscount(int id, [FromBody] ProductsDiscountsRequest request)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<ProductDiscounts>(APIType.Client, "Products/Discount", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("Discount/{id}")]
        public async Task<IActionResult> DeleteProductDiscount(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<ProductDiscounts>(APIType.Client, "Products/Discount", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("Discount/Deactive/{id}")]
        public async Task<IActionResult> DeactiveProductDiscount(int id)
        {
            try
            {
                //Get the AuthToken
                string authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.PutResponse<BaseAPIResponse>(APIType.Client, "Products/Discount/Deactive", id, "", authToken);
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

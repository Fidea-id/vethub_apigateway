using Application.Services.Contracts;
using Domain.Entities;
using Domain.Entities.Responses;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Application.Services.Implementations
{
    public class RestAPIService : IRestAPIService
    {
        private IUriService _uriService;
        private readonly ILogger<RestAPIService> _logger;
        //private readonly RestClient _restClient;
        private readonly HttpClient _httpClient;

        public RestAPIService(IUriService uriService, HttpClient httpClient,
            ILoggerFactory loggerFactory)
        {
            _uriService = uriService;
            _httpClient = httpClient;
            _logger = loggerFactory.CreateLogger<RestAPIService>();
        }

        //public async Task<T> GetResponse<T>(APIType type, string url, string auth = null) where T : class
        //{
        //    Uri getUrl = _uriService.GetAPIUri(type);
        //    var request = new RestRequest(getUrl + url, Method.Get);
        //    if (auth != null)
        //    {
        //        request.AddHeader("Authorization", auth);
        //    }
        //    var response = await _restClient.ExecuteAsync(request);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        await ThrowError(response, $"{type}");
        //    }
        //    _logger.LogInformation("Success Get Response " + type, getUrl.ToString());
        //    return JsonConvert.DeserializeObject<T>(response.Content) ?? default;
        //}
        //public async Task<T> GetResponseWithCTS<T>(APIType type, string url, string auth = null, TimeSpan? timeout = null) where T : class
        //{
        //    try
        //    {

        //        Uri getUrl = _uriService.GetAPIUri(type);
        //        _logger.LogInformation("Start Get Response " + type + " - " + getUrl + url);
        //        var request = new RestRequest(getUrl + url, Method.Get);
        //        if (auth != null)
        //        {
        //            request.AddHeader("Authorization", auth);
        //        }

        //        if (timeout.HasValue)
        //        {
        //            request.Timeout = (int)timeout.Value.TotalMilliseconds;
        //        }
        //        else
        //        {
        //            request.Timeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;
        //        }

        //        var response = await _restClient.ExecuteAsync(request);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            await ThrowError(response, $"{type}");
        //        }
        //        _logger.LogInformation("Success Get Response " + type + " - " + getUrl + url);
        //        return JsonConvert.DeserializeObject<T>(response.Content) ?? default;
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        _logger.LogError($"HttpClient request failed: {ex.Message}");
        //        throw;
        //    }
        //}
        //public async Task<T> GetResponseFilter<T, TFilter>(APIType type, string url, string auth = null, TFilter queryParams = null) where T : class where TFilter : BaseEntityFilter
        //{
        //    Uri getUrl = _uriService.GetAPIUri(type);
        //    var builder = new UriBuilder(getUrl + url);
        //    var queryParameters = HttpUtility.ParseQueryString(builder.Query);

        //    if (queryParams != null)
        //    {
        //        var filterProperties = queryParams.GetType().GetProperties();
        //        foreach (var property in filterProperties)
        //        {
        //            var value = property.GetValue(queryParams);
        //            if (value != null)
        //            {
        //                var propertyName = property.Name;
        //                queryParameters[property.Name] = value.ToString();
        //            }
        //        }
        //    }
        //    if (queryParams?.Take.HasValue == true)
        //    {
        //        queryParameters["Take"] = queryParams.Take.Value.ToString();
        //    }
        //    if (queryParams?.Skip.HasValue == true)
        //    {
        //        queryParameters["Skip"] = queryParams.Skip.Value.ToString();
        //    }

        //    builder.Query = queryParameters.ToString();
        //    string requestUrl = builder.ToString();

        //    var request = new RestRequest(requestUrl, Method.Get);
        //    if (auth != null)
        //    {
        //        request.AddHeader("Authorization", auth);
        //    }
        //    var response = await _restClient.ExecuteAsync(request);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        await ThrowError(response, $"{type}");
        //    }
        //    _logger.LogInformation("Success Get Response Filter " + type, getUrl.ToString());
        //    return JsonConvert.DeserializeObject<T>(response.Content) ?? default;
        //}

        //public async Task<T> PostResponse<T>(APIType type, string url, string obj, string auth = null) where T : class
        //{
        //    Uri getUrl = _uriService.GetAPIUri(type);

        //    var request = new RestRequest(getUrl + url, Method.Post);
        //    if (auth != null)
        //    {
        //        request.AddHeader("Authorization", auth);
        //    }

        //    if (!string.IsNullOrEmpty(obj))
        //    {
        //        request.AddHeader("Content-Type", "application/json");
        //        request.AddBody(obj);
        //    }

        //    var response = await _restClient.ExecuteAsync(request);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        await ThrowError(response, $"{type}");
        //    }

        //    _logger.LogInformation("Success Post Response " + type, getUrl.ToString());

        //    return JsonConvert.DeserializeObject<T>(response.Content) ?? default;
        //}

        //public async Task<T> PostResponseWithCTS<T>(APIType type, string url, string obj, string auth = null, TimeSpan? timeout = null) where T : class
        //{
        //    Uri getUrl = _uriService.GetAPIUri(type);

        //    try
        //    {
        //        _logger.LogInformation("Start Post Response " + type + " - " + getUrl + url);
        //        var request = new RestRequest(getUrl + url, Method.Post);
        //        if (auth != null)
        //        {
        //            request.AddHeader("Authorization", auth);
        //        }

        //        if (timeout.HasValue)
        //        {
        //            request.Timeout = (int)timeout.Value.TotalMilliseconds;
        //        }
        //        else
        //        {
        //            request.Timeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;
        //        }

        //        if (!string.IsNullOrEmpty(obj))
        //        {
        //            request.AddHeader("Content-Type", "application/json");
        //            request.AddBody(obj);
        //        }

        //        var response = await _restClient.ExecuteAsync(request);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            await ThrowError(response, $"{type}");
        //        }

        //        _logger.LogInformation("Success Get Response " + type + " - " + getUrl + url);

        //        return JsonConvert.DeserializeObject<T>(response.Content) ?? default;
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        _logger.LogError($"HttpClient request failed: {ex.Message}");
        //        throw;
        //    }
        //}

        //public async Task<T> PutResponse<T>(APIType type, string url, int id, string obj, string auth = null) where T : class
        //{
        //    Uri getUrl = _uriService.GetAPIUri(type);

        //    var request = new RestRequest($"{getUrl + url}/{id}", Method.Put);

        //    if (auth != null)
        //    {
        //        request.AddHeader("Authorization", auth);
        //    }

        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddBody(obj);

        //    var response = await _restClient.ExecuteAsync(request);
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        await ThrowError(response, $"{type}");
        //    }
        //    _logger.LogInformation("Success Put Response " + type + "-" + $"{getUrl + url}/{id}");
        //    return JsonConvert.DeserializeObject<T>(response.Content) ?? default;
        //}

        //public async Task<T> DeleteResponse<T>(APIType type, string url, int id, string auth = null) where T : class
        //{
        //    Uri getUrl = _uriService.GetAPIUri(type);
        //    var request = new RestRequest($"{getUrl + url}/{id}", Method.Delete);

        //    if (auth != null)
        //    {
        //        request.AddHeader("Authorization", auth);
        //    }

        //    var response = await _restClient.ExecuteAsync(request);
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        var mssg = response.Content;
        //        _logger.LogError($"{mssg}-{getUrl + url}/{id}");
        //        await ThrowError(response, $"{type}");
        //    }
        //    _logger.LogInformation("Success Delete Response " + type, getUrl.ToString());
        //    return JsonConvert.DeserializeObject<T>(response.Content) ?? default;
        //}

        //public async Task<T> GetPlaceResponse<T>(string url, string query) where T : class
        //{
        //    var api_key = "4ecb32b3-4fd0-58bf-09bf-a557aabb";
        //    var base_url = $"https://api.goapi.io/regional/{url}?api_key={api_key}&{query}";

        //    var request = new RestRequest(base_url, Method.Get);
        //    var response = await _restClient.ExecuteAsync(request);
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        await ThrowError(response, "Go");
        //    }
        //    return JsonConvert.DeserializeObject<T>(response.Content) ?? default;
        //}

        public async Task<T> GetResponse<T>(APIType type, string url, string auth = null) where T : class
        {
            Uri getUrl = _uriService.GetAPIUri(type);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, getUrl + url);
            if (auth != null)
            {
                request.Headers.Add("Authorization", auth);
            }
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                await ThrowError(response, $"{type}");
            }
            _logger.LogInformation("Success Get Response " + type, getUrl.ToString());
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) ?? default;
        }
        public async Task<T> GetResponseWithCTS<T>(APIType type, string url, string auth = null, TimeSpan? timeout = null) where T : class
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout.HasValue)
                    {
                        client.Timeout = timeout.Value;
                    }
                    else
                    {
                        client.Timeout = TimeSpan.FromMinutes(5);
                    }

                    Uri getUrl = _uriService.GetAPIUri(type);
                    _logger.LogInformation("Start Get Response " + type + " - " + getUrl + url);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, getUrl + url);
                    if (auth != null)
                    {
                        request.Headers.Add("Authorization", auth);
                    }
                    HttpResponseMessage response = await client.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        await ThrowError(response, $"{type}");
                    }
                    _logger.LogInformation("Success Get Response " + type + " - " + getUrl + url);
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) ?? default;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HttpClient request failed: {ex.Message}");
                throw;
            }
        }
        public async Task<T> GetResponseFilter<T, TFilter>(APIType type, string url, string auth = null, TFilter queryParams = null) where T : class where TFilter : BaseEntityFilter
        {
            Uri getUrl = _uriService.GetAPIUri(type);
            var builder = new UriBuilder(getUrl + url);
            var queryParameters = HttpUtility.ParseQueryString(builder.Query);

            if (queryParams != null)
            {
                var filterProperties = queryParams.GetType().GetProperties();
                foreach (var property in filterProperties)
                {
                    var value = property.GetValue(queryParams);
                    if (value != null)
                    {
                        var propertyName = property.Name;
                        queryParameters[property.Name] = value.ToString();
                    }
                }
            }
            if (queryParams?.Take.HasValue == true)
            {
                queryParameters["Take"] = queryParams.Take.Value.ToString();
            }
            if (queryParams?.Skip.HasValue == true)
            {
                queryParameters["Skip"] = queryParams.Skip.Value.ToString();
            }

            builder.Query = queryParameters.ToString();
            string requestUrl = builder.ToString();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            if (auth != null)
            {
                request.Headers.Add("Authorization", auth);
            }

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                await ThrowError(response, $"{type}");
            }
            _logger.LogInformation("Success Get Response Filter " + type, getUrl.ToString());
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) ?? default;
        }

        public async Task<T> PostResponse<T>(APIType type, string url, string obj, string auth = null) where T : class
        {
            Uri getUrl = _uriService.GetAPIUri(type);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, getUrl + url);

            if (!string.IsNullOrEmpty(obj))
            {
                request.Content = new StringContent(obj, Encoding.UTF8, "application/json");
            }

            if (auth != null)
            {
                request.Headers.Add("Authorization", auth);
            }

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                await ThrowError(response, $"{type}");
            }

            _logger.LogInformation("Success Post Response " + type, getUrl.ToString());

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) ?? default;
        }

        public async Task<T> PostResponseWithCTS<T>(APIType type, string url, string obj, string auth = null, TimeSpan? timeout = null) where T : class
        {
            Uri getUrl = _uriService.GetAPIUri(type);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout.HasValue)
                    {
                        client.Timeout = timeout.Value;
                    }
                    else
                    {
                        client.Timeout = TimeSpan.FromMinutes(5);
                    }

                    _logger.LogInformation("Start Post Response " + type + " - " + getUrl + url);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, getUrl + url);

                    if (!string.IsNullOrEmpty(obj))
                    {
                        request.Content = new StringContent(obj, Encoding.UTF8, "application/json");
                    }

                    if (auth != null)
                    {
                        request.Headers.Add("Authorization", auth);
                    }

                    HttpResponseMessage response = await client.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        await ThrowError(response, $"{type}");
                    }

                    _logger.LogInformation("Success Get Response " + type + " - " + getUrl + url);

                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) ?? default;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HttpClient request failed: {ex.Message}");
                throw;
            }
        }

        public async Task<T> PutResponse<T>(APIType type, string url, int id, string obj, string auth = null) where T : class
        {
            Uri getUrl = _uriService.GetAPIUri(type);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"{getUrl + url}/{id}");
            request.Content = new StringContent(obj, Encoding.UTF8, "application/json");
            if (auth != null)
            {
                request.Headers.Add("Authorization", auth);
            }
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                await ThrowError(response, $"{type}");
            }
            _logger.LogInformation("Success Put Response " + type + "-" + $"{getUrl + url}/{id}");
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) ?? default;
        }

        public async Task<T> DeleteResponse<T>(APIType type, string url, int id, string auth = null) where T : class
        {
            Uri getUrl = _uriService.GetAPIUri(type);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"{getUrl + url}/{id}");
            if (auth != null)
            {
                request.Headers.Add("Authorization", auth);
            }
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var mssg = await response.Content.ReadAsStringAsync();
                _logger.LogError($"{mssg}-{getUrl + url}/{id}");
                await ThrowError(response, $"{type}");
            }
            _logger.LogInformation("Success Delete Response " + type, getUrl.ToString());
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) ?? default;
        }

        public async Task<T> GetPlaceResponse<T>(string url, string query) where T : class
        {
            var api_key = "4ecb32b3-4fd0-58bf-09bf-a557aabb";
            var base_url = $"https://api.goapi.io/regional/{url}?api_key={api_key}&{query}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, base_url);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                await ThrowError(response, "Go");
            }
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) ?? default;
        }

        private async Task ThrowError(HttpResponseMessage response, string type)
        {
            var errors = JsonConvert.DeserializeObject<ErrorResponseModel>(await response.Content.ReadAsStringAsync());
            var message = errors.Errors[0].Message;
            _logger.LogError($"{type}API-{errors.Errors[0].Field}-{message}");
            var newExc = new Exception($"{message}");
            newExc.Source = $"{type}API-{errors.Errors[0].Field}";
            throw newExc;
        }
        //private async Task ThrowError(RestResponse response, string type)
        //{
        //    var errors = JsonConvert.DeserializeObject<ErrorResponseModel>(response.Content);
        //    var message = errors.Errors[0].Message;
        //    _logger.LogError($"{type}API-{errors.Errors[0].Field}-{message}");
        //    var newExc = new Exception($"{message}");
        //    newExc.Source = $"{type}API-{errors.Errors[0].Field}";
        //    throw newExc;
        //}
    }
}

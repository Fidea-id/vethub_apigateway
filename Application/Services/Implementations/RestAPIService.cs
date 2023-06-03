using Application.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Domain.Entities.Responses;

namespace Application.Services.Implementations
{
    public class RestAPIService: IRestAPIService
    {
        private IUriService _uriService;
        public RestAPIService(IUriService uriService)
        {
            _uriService = uriService;
        }

        public async Task<T> GetResponse<T>(string type, string url, string auth = null) where T:class
        {
            Uri getUrl;
            if (type == "Master")
            {
                getUrl = _uriService.GetMasterAPIUri();
            }
            else if (type == "Client")
            {
                getUrl = _uriService.GetClientAPIUri();
            }
            else
            {
                throw new Exception("Invalid type request!");
            }

            var options = new RestClientOptions(getUrl);
            var client = new RestClient(options);

            var request = new RestRequest(url, Method.Get).AddHeader("Content-Type", "application/json");
            if (auth != null)
            {
                request.AddHeader("Authorization", auth);
            }
            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                var errors = JsonConvert.DeserializeObject<ErrorResponseModel>(response.Content);
                throw new Exception(errors.Errors[0].Message, errors.Errors[0].Detail);
            }

            return JsonConvert.DeserializeObject<T>(response.Content) ?? default;
        }

        public async Task<T> PostResponse<T>(string type, string url, string obj, string auth = null) where T : class
        {
            Uri getUrl;
            if(type == "Master")
            {
                getUrl = _uriService.GetMasterAPIUri();
            }
            else if (type == "Client")
            {
                getUrl = _uriService.GetClientAPIUri();
            }
            else
            {
                throw new Exception("Invalid type request!");
            }

            var options = new RestClientOptions(getUrl);
            var client = new RestClient(options);

            var request = new RestRequest(url, Method.Post).AddJsonBody(obj).AddHeader("Content-Type", "application/json");
            if(auth != null)
            {
                request.AddHeader("Authorization", auth);
            }
            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                var errors = JsonConvert.DeserializeObject<ErrorResponseModel>(response.Content);
                throw new Exception(errors.Errors[0].Message, errors.Errors[0].Detail);
            }

            return JsonConvert.DeserializeObject<T>(response.Content) ?? default;
        }
    }
}

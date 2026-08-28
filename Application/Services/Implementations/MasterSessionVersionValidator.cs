using Application.Services.Contracts;
using Domain.Entities;
using Domain.Entities.Responses.Masters;
using System.Net.Http.Json;

namespace Application.Services.Implementations
{
    public class MasterSessionVersionValidator : ISessionVersionValidator
    {
        private readonly HttpClient _httpClient;
        private readonly IUriService _uriService;

        public MasterSessionVersionValidator(HttpClient httpClient, IUriService uriService)
        {
            _httpClient = httpClient;
            _uriService = uriService;
        }

        public async Task<bool> IsValidAsync(int userId, int sessionVersion, CancellationToken cancellationToken = default)
        {
            var uri = new Uri(_uriService.GetAPIUri(APIType.Master), $"Auth/Session/Version/{userId}");
            using var response = await _httpClient.GetAsync(uri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var currentSession = await response.Content.ReadFromJsonAsync<SessionVersionResponse>(cancellationToken: cancellationToken);
            return currentSession?.SessionVersion == sessionVersion;
        }
    }
}

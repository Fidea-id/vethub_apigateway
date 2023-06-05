using Application.Services.Contracts;
using Domain.Entities.Requests;
using Domain.Entities.Responses;
using Newtonsoft.Json;

namespace Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private IRestAPIService _restAPIService;
        public AuthService(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        public async Task<LoginResponse> LoginAsync(UserLoginRequest data)
        {
            try
            {
                var requestJson = JsonConvert.SerializeObject(data);
                var response = await _restAPIService.PostResponse<LoginResponse>("Master", "Auth/Login", requestJson);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<RegisterResponse> RegisterUserAsync(UserRegisterRequest data)
        {
            try
            {
                var newDBName = "VetHub_Client_" + Guid.NewGuid().ToString();
                data.Entity = newDBName;
                var requestJson = JsonConvert.SerializeObject(data);
                var response = await _restAPIService.PostResponse<RegisterResponse>("Master", "Auth/Register", requestJson);
                var generateDB = await _restAPIService.GetResponse<RegisterResponse>("Client", "Master/GenerateInitDB/" + newDBName);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<UserProfileResponse> GetByNameOrEmailAsync(string value, string auth)
        {
            throw new NotImplementedException();
        }

        public async Task<UserProfileResponse> GetUserProfileByIdAsync(int id, string auth)
        {
            try
            {
                var response = await _restAPIService.GetResponse<UserProfileResponse>("Master", "Auth/User/" + id, auth);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

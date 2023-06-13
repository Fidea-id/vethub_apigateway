using Application.Services.Contracts;
using Domain.Entities;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Masters;
using Domain.Entities.Responses;
using Domain.Entities.Responses.Masters;
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
                var response = await _restAPIService.PostResponse<LoginResponse>(APIType.Master, "Auth/Login", requestJson);
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
                var newDBName = "VetHub_Client_" + Guid.NewGuid().ToString().Replace("-", "");
                data.Entity = newDBName;
                var requestJson = JsonConvert.SerializeObject(data);

                //register
                var response = await _restAPIService.PostResponse<RegisterResponse>(APIType.Master, "Auth/Register", requestJson);
                //create db
                var generateDB = await _restAPIService.GetResponse<string>(APIType.Client, "Master/GenerateInitDB/" + newDBName);
                //insert user profile
                var newProfile = new Profile
                {
                    Email = data.Email,
                    Entity = newDBName,
                    GlobalId = response.Id,
                    Name = "",
                    Photo = "",
                    Roles = data.Roles
                };
                var profileJson = JsonConvert.SerializeObject(newProfile);
                var userProfile = await _restAPIService.PostResponse<UserProfileResponse>(APIType.Client, $"Profile/public/{data.Entity}", profileJson);
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
                var response = await _restAPIService.GetResponse<UserProfileResponse>(APIType.Master, "Auth/User/" + id, auth);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

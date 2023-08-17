using Application.Services.Contracts;
using Domain.Entities;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Requests.Masters;
using Domain.Entities.Responses;
using Domain.Entities.Responses.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private IRestAPIService _restAPIService;
        private readonly ILogger<AuthService> _logger;
        private IFileUploadService _fileUploadService;

        public AuthService(IRestAPIService restAPIService, IFileUploadService fileUploadService,
            ILoggerFactory loggerFactory)
        {
            _fileUploadService = fileUploadService;
            _restAPIService = restAPIService;
            _logger = loggerFactory.CreateLogger<AuthService>();
        }

        public async Task<LoginResponse> LoginAsync(UserLoginRequest data)
        {
            try
            {
                var requestJson = JsonConvert.SerializeObject(data);
                var response = await _restAPIService.PostResponse<LoginResponse>(APIType.Master, "Auth/Login", requestJson);
                //update schema db client
                if (response.Roles != "Superadmin")
                {
                    var generateDB = await _restAPIService.GetResponse<BaseAPIResponse>(APIType.Client, "Master/CheckSchemeDB", "Bearer " + response.SessionToken);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<RegisterResponse> RegisterUserAsync(FullRegisterClinicRequest data, string auth)
        {
            try
            {
                var newDBName = "VetHub_Client_" + Guid.NewGuid().ToString().Replace("-", "");
                data.ClinicData.Entity = newDBName;
                var registerRequestJson = JsonConvert.SerializeObject(data);

                //register
                var response = await _restAPIService.PostResponse<RegisterResponse>(APIType.Master, "Auth/Register", registerRequestJson);
                if (response.Roles != "Superadmin")
                {
                    //create db client
                    var generateDB = await _restAPIService.GetResponse<BaseAPIResponse>(APIType.Client, "Master/GenerateInitDB/" + newDBName);
                    //insert user profile
                    var newProfile = new Profile
                    {
                        Email = data.OwnerData.Email,
                        Entity = newDBName,
                        GlobalId = response.Id,
                        Name = data.OwnerData.Name,
                        Photo = data.OwnerData.Photo,
                        Roles = "Owner"
                    };
                    var profileJson = JsonConvert.SerializeObject(newProfile);
                    var userProfile = await _restAPIService.PostResponse<UserProfileResponse>(APIType.Client, $"Profile/public/{newDBName}", profileJson);
                    var initField = await _restAPIService.GetResponse<BaseAPIResponse>(APIType.Client, $"Master/GenerateInitDBField/{newDBName}");

                    var clinicRequestJson = JsonConvert.SerializeObject(data.ClinicData);
                    var userClinic = await _restAPIService.PostResponse<Clinics>(APIType.Client, $"Data/ClinicsEntity/" + newDBName, clinicRequestJson, auth);
                }
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
                //get profile client data
                var responseClient = await _restAPIService.GetResponse<UserProfileResponse>(APIType.Client, "Profile/User/" + response.Id, auth);
                //return combine profile
                return CombineMasterClientProfile(response, responseClient);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private UserProfileResponse CombineMasterClientProfile(UserProfileResponse master, UserProfileResponse client)
        {

            //client profile data to global profile
            master.GlobalId = master.Id;
            master.Id = client.Id;
            master.Photo = client.Photo;
            return master;
        }

        public async Task<BaseAPIResponse> CheckUserActivationAsync(ActivationRequest request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<BaseAPIResponse>(APIType.Master, "Auth/CheckUserActivation", json);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseAPIResponse> UserActivationAsync(ActivationRequest request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<BaseAPIResponse>(APIType.Master, "Auth/UserActivation", json);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UploadProfilePicture(IFormFile file, string folder)
        {
            var upload = await _fileUploadService.UploadImageAsync(file, folder);
            return upload;
        }

        public async Task<UserProfileResponse> UpdateProfile(int id, ProfileRequest request, string auth)
        {
            try
            {
                var response = await _restAPIService.GetResponse<UserProfileResponse>(APIType.Master, "Auth/User/" + id, auth);
                //get profile client data
                string obj = JsonConvert.SerializeObject(request);
                //update global profile
                var responseGlobal = await _restAPIService.PutResponse<UserProfileResponse>(APIType.Master, "Auth/User", response.Id, obj, auth);
                if (responseGlobal != null)
                {
                    //update profile client
                    var responseClient = await _restAPIService.PutResponse<UserProfileResponse>(APIType.Client, "Profile/GlobalId", response.Id, obj, auth);
                    //combine profile
                    response = CombineMasterClientProfile(responseGlobal, responseClient);
                }

                if (request.Email != null)
                {
                    //TODO:send email confirmation
                }

                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task<BaseAPIResponse> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            try
            {
                string obj = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<BaseAPIResponse>(APIType.Master, "Auth/ForgotPassword/", obj);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseAPIResponse> ChangeForgotPasswordAsync(ForgotPasswordRequest request)
        {
            try
            {
                string obj = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<BaseAPIResponse>(APIType.Master, "Auth/ChangeForgotPassword/", obj);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseAPIResponse> ChangePasswordAsync(ForgotPasswordRequest request, string auth)
        {
            try
            {
                string obj = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<BaseAPIResponse>(APIType.Master, "Auth/ChangePassword/", obj, auth);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

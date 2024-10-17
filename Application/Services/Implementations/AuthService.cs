using Application.Services.Contracts;
using Domain.Entities;
using Domain.Entities.DTOs.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Models.Masters;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Requests.Masters;
using Domain.Entities.Responses;
using Domain.Entities.Responses.Masters;
using Hangfire;
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
                //if (response.Roles != "Superadmin")
                //{
                //    var generateDB = await _restAPIService.GetResponse<BaseAPIResponse>(APIType.Client, "Master/CheckSchemeDB", "Bearer " + response.SessionToken);
                //}
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<BaseAPIResponse> DemoAsync(UserDemoRequest data)
        {
            try
            {
                var requestJson = JsonConvert.SerializeObject(data);
                var response = await _restAPIService.PostResponse<BaseAPIResponse>(APIType.Master, "Auth/Demo", requestJson);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ProccessAddProfile(IEnumerable<RegisterResponse> response, ClinicsRequest data, string newDBName, string auth, TimeSpan timespan)
        {
            try
            {
                _logger.LogInformation("Background job start");
                var listProfile = new List<Profile>();
                _logger.LogInformation($"Define profile data");
                foreach (var item in response)
                {
                    //insert user profile
                    var newProfile = new Profile
                    {
                        Email = item.Email,
                        Entity = newDBName,
                        GlobalId = item.Id,
                        Name = item.Name,
                        Photo = null,
                        Roles = item.Roles
                    };
                    listProfile.Add(newProfile);
                }
                var newCLinicProfile = new RegisterClinicProfileRequest
                {
                    ClinicData = data,
                    ProfileData = listProfile
                };

                var clinicRequestJson = JsonConvert.SerializeObject(newCLinicProfile);
                _logger.LogInformation($"Start create clinic data," + clinicRequestJson);
                var userClinic = await _restAPIService.PostResponseWithCTS<Clinics>(APIType.Client, $"Data/InitClinicsProfile/" + newDBName, clinicRequestJson, auth, timespan);
                _logger.LogInformation($"Done create clinic data");

                _logger.LogInformation("Background job done");
            }
            catch
            {
                throw;
            }
        }
        public async Task ProccessInitFieldDB(string newDBName, TimeSpan timespan)
        {
            _logger.LogInformation($"Start init db field");
            var initField = await _restAPIService.GetResponseWithCTS<BaseAPIResponse>(APIType.Client, $"Master/GenerateInitDBField/{newDBName}", null, timespan);
            _logger.LogInformation($"Done init db field");
        }

        //TODO: buat event log register jadi 1 saja
        public async Task ProcessAdditionalLogic(IEnumerable<RegisterResponse> response, FullRegisterClinicRequest data, string auth, string newDBName)
        {
            try
            {
                var timespan = TimeSpan.FromMinutes(10);
                if (response.Any(x => x.Roles != "Superadmin"))
                {
                    _logger.LogInformation("Background job start");

                    var listProfile = new List<Profile>();
                    _logger.LogInformation($"Define profile data");
                    foreach (var item in response)
                    {
                        //insert user profile
                        var newProfile = new Profile
                        {
                            Email = item.Email,
                            Entity = newDBName,
                            GlobalId = item.Id,
                            Name = item.Name,
                            Photo = "https://app.vethub.id/images/users/default/ManA.png",
                            Roles = item.Roles
                        };
                        listProfile.Add(newProfile);
                    }
                    var newCLinicProfile = new RegisterClinicProfileRequest
                    {
                        ClinicData = data.ClinicData,
                        ProfileData = listProfile
                    };

                    var clinicRequestJson = JsonConvert.SerializeObject(newCLinicProfile);
                    _logger.LogInformation($"Start create clinic data," + clinicRequestJson);
                    var generateDB = await _restAPIService.PostResponseWithCTS<BaseAPIResponse>(APIType.Client, "Master/GenerateInitDBClient/" + newDBName, clinicRequestJson, auth, timespan);
                    _logger.LogInformation($"Done init db with name: {newDBName}");

                    _logger.LogInformation("Background job done");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions in the background process
                _logger.LogInformation($"Background job failed: {ex.Message}");
            }
        }

        public async Task<IEnumerable<RegisterResponse>> RegisterUserAsync(FullRegisterClinicRequest data, string auth)
        {
            try
            {
                var newDBName = "VetHub_Client_" + Guid.NewGuid().ToString().Replace("-", "");
                data.ClinicData.Entity = newDBName;
                var registerRequestJson = JsonConvert.SerializeObject(data);

                _logger.LogInformation("Try Register with data:" + registerRequestJson);
                //register
                var response = await _restAPIService.PostResponse<IEnumerable<RegisterResponse>>(APIType.Master, "Auth/Register", registerRequestJson);
                // Enqueue a background job to process the additional logic
                BackgroundJob.Enqueue(() => ProcessAdditionalLogic(response, data, auth, newDBName));

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

        public async Task<BaseAPIResponse> ResendEmailVerif(int id, string auth)
        {
            try
            {
                var users = await _restAPIService.GetResponse<Users>(APIType.Master, "Auth/UsersData/" + id, auth);
                var clinic = await _restAPIService.GetResponse<Clinics>(APIType.Client, "Data/ClinicsEntity/" + users.Entity, auth);
                var request = new ResendEmailVerifRequest()
                {
                    UserData = users,
                    ClinicName = clinic.Name
                };

                var response = await _restAPIService.PostResponse<BaseAPIResponse>(APIType.Master, "Auth/ResendEmailVerificaiton/", JsonConvert.SerializeObject(request), auth);

                return response;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while send email verification : " + e.Message);
                throw;
            }
        }

        public async Task<UserProfileResponse> GetUserProfileByIdAsync(int id, string auth)
        {
            try
            {
                var response = await _restAPIService.GetResponse<UserProfileResponse>(APIType.Master, "Auth/User/" + id, auth);
                if (response.Roles != "Superadmin")
                {
                    //get profile client data
                    var responseClient = await _restAPIService.GetResponse<UserProfileResponse>(APIType.Client, "Profile/User/" + response.Id, auth);
                    //return combine profile
                    return CombineMasterClientProfile(response, responseClient);
                }
                return CombineMasterClientProfile(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private UserProfileResponse CombineMasterClientProfile(UserProfileResponse master, UserProfileResponse client = null)
        {
            //client profile data to global profile
            master.GlobalId = master.Id;
            if (client != null)
            {
                master.Id = client.Id;
                master.Photo = client.Photo;
            }
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

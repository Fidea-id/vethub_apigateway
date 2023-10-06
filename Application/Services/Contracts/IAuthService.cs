using Domain.Entities.Requests.Clients;
using Domain.Entities.Requests.Masters;
using Domain.Entities.Responses;
using Domain.Entities.Responses.Masters;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Contracts
{
    public interface IAuthService
    {
        public Task<LoginResponse> LoginAsync(UserLoginRequest data);
        public Task<BaseAPIResponse> DemoAsync(UserDemoRequest data);
        public Task<RegisterResponse> RegisterUserAsync(FullRegisterClinicRequest data, string auth);
        public Task<UserProfileResponse> GetByNameOrEmailAsync(string value, string auth);
        public Task<UserProfileResponse> GetUserProfileByIdAsync(int id, string auth);
        public Task<BaseAPIResponse> CheckUserActivationAsync(ActivationRequest request);
        public Task<BaseAPIResponse> UserActivationAsync(ActivationRequest request);
        public Task<string> UploadProfilePicture(IFormFile file, string path);
        public Task<UserProfileResponse> UpdateProfile(int id, ProfileRequest request, string auth);
        public Task<BaseAPIResponse> ForgotPasswordAsync(ForgotPasswordRequest request);
        public Task<BaseAPIResponse> ChangeForgotPasswordAsync(ForgotPasswordRequest request);
        public Task<BaseAPIResponse> ChangePasswordAsync(ForgotPasswordRequest request, string auth);
    }
}

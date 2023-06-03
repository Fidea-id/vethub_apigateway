using Domain.Entities.Requests;
using Domain.Entities.Responses;

namespace Application.Services.Contracts
{
    public interface IAuthService
    {
        public Task<LoginResponse> LoginAsync(UserLoginRequest data);
        public Task<RegisterResponse> RegisterUserAsync(UserRegisterRequest data);
        public Task<UserProfileResponse> GetByNameOrEmailAsync(string value, string auth);
        public Task<UserProfileResponse> GetUserProfileByIdAsync(int id, string auth);
    }
}

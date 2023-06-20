using Domain.Entities.Requests.Masters;
using Domain.Entities.Responses;
using Domain.Entities.Responses.Masters;

namespace Application.Services.Contracts
{
    public interface IAuthService
    {
        public Task<LoginResponse> LoginAsync(UserLoginRequest data);
        public Task<RegisterResponse> RegisterUserAsync(FullRegisterClinicRequest data);
        public Task<UserProfileResponse> GetByNameOrEmailAsync(string value, string auth);
        public Task<UserProfileResponse> GetUserProfileByIdAsync(int id, string auth);
    }
}

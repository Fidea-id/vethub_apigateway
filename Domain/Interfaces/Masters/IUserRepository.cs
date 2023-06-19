using Domain.Entities.DTOs.Masters;
using Domain.Entities.Filters.Masters;
using Domain.Entities.Models.Masters;

namespace Domain.Interfaces.Masters
{
    public interface IUserRepository : IGenericRepository<Users, UsersFilter>
    {
        Task<bool> CheckPassword(int userId, string password);
        Task UpdatePassword(int userId, string newPassword);
        Task<IEnumerable<Roles>> GetRoles();
        Task<Roles> GetRoleById(int id);
        Task<IEnumerable<VerifiedUserDTO>> GetVerifiedUsers();
    }
}

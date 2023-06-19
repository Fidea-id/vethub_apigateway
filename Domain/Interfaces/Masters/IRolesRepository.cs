using Domain.Entities.Filters.Masters;
using Domain.Entities.Models.Masters;

namespace Domain.Interfaces.Masters
{
    public interface IRolesRepository : IGenericRepository<Roles, RolesFilter>
    {
    }
}

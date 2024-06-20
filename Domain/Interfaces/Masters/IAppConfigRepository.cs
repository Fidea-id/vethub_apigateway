using Domain.Entities;
using Domain.Entities.Models.Masters;

namespace Domain.Interfaces.Masters
{
    public interface IAppConfigRepository : IGenericRepository<AppConfig, BaseEntityFilter>
    {
    }
}

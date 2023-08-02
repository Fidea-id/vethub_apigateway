using Domain.Entities.Filters;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IDiagnosesRepository : IGenericRepository<Diagnoses, NameBaseEntityFilter>
    {
    }
}

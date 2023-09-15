using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IPrescriptionFrequentsRepository : IGenericRepository<PrescriptionFrequents, PrescriptionFrequentsFilter>
    {
    }
}

using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IClinicRepository : IGenericRepository<Clinics, ClinicsFilter>
    {
    }
}

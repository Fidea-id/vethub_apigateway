using Domain.Entities.DTOs.Clients;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IClinicRepository : IGenericRepository<Clinics, ClinicsFilter>
    {
        Task<ClinicReportsClientDTO> GetClinicReportsAsync(string db);
    }
}

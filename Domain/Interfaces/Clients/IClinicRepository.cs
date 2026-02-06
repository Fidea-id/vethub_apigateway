using Domain.Entities.DTOs.Clients;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IClinicRepository : IGenericRepository<Clinics, ClinicsFilter>
    {
        Task<IEnumerable<ClinicProductUsageDTO>> GetClinicProductUsageReportsAsync(string db);
        Task<IEnumerable<ClinicServiceUsageDTO>> GetClinicServiceUsageReportsAsync(string db);
        Task<IEnumerable<ClinicAnimalUsageDTO>> GetClinicAnimalUsageReportsAsync(string db);
        Task<ClinicReportsClientDTO> GetClinicReportsAsync(string db);
    }
}

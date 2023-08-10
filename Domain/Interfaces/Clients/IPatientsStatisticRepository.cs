using Domain.Entities.DTOs.Clients;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IPatientsStatisticRepository : IGenericRepository<PatientsStatistic, PatientsStatisticFilter>
    {
        Task<IEnumerable<PatientsStatisticDto>> ReadPatientsStatisticAsync(int patientId, string dbName);
    }
}

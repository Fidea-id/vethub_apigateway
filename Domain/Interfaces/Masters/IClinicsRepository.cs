using Domain.Entities.Filters.Masters;
using Domain.Entities.Models.Masters;
using Domain.Entities.Responses.Masters;

namespace Domain.Interfaces.Masters
{
    public interface IClinicsRepository : IGenericRepository<Clinics, ClinicsFilter>
    {
        Task<IEnumerable<ClientClinicListResponse>> GetList();
        Task<ClientClinicDetailResponse> GetDetail(int id);
    }
}

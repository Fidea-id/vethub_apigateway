using Domain.Entities;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IMixedMedicineRepository : IGenericRepository<MixedMedicine, BaseEntityFilter>
    {
        Task<IEnumerable<MixedMedicineDetailResponse>> GetDetails(string dbName);
        Task<MixedMedicineDetailResponse> GetDetailById(string dbName, int id);
        Task<MixedMedicineDetailResponse> CreateMixedMedicine(string dbName, MixedMedicineDetailRequest data);
        Task<MixedMedicineDetailResponse> UpdateMixedMedicine(string dbName, int id, MixedMedicineDetailRequest data);
        Task DeleteMixedMedicine(string dbName, int id);
    }
}

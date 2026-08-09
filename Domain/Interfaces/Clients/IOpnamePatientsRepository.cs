using Domain.Entities.DTOs;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IOpnamePatientsRepository : IGenericRepository<OpnamePatients, OpnamePatientsFilter>
    {
        Task<DataResultDTO<OpnamePatients>> GetByMedId(string dbName, int id);
        Task<DataResultDTO<OpnamePatients>> GetByOpnameId(string dbName, int id);
        Task<DataResultDTO<OpnamePatientsDetailResponse>> GetDetailList(string dbName, OpnamePatientsFilter filter);
    }
}

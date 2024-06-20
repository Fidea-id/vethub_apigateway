using Domain.Entities.DTOs;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IOpnamePatientsRepository : IGenericRepository<OpnamePatients, OpnamePatientsFilter>
    {
        Task<DataResultDTO<OpnamePatients>> GetByMedId(string dbName, int id);
        Task<DataResultDTO<OpnamePatients>> GetByOpnameId(string dbName, int id);
    }
}

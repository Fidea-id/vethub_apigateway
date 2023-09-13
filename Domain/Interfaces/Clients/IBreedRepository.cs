using Domain.Entities.DTOs;
using Domain.Entities.Filters;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IBreedRepository : IGenericRepository<Breeds, NameBaseEntityFilter>
    {
        Task<Breeds> GetByName(string dbName, int speciesId, string name);
        Task<BreedAnimalResponse> GetBreedAnimal(int id, string dbName);
        Task<IEnumerable<BreedAnimalResponse>> GetBreedAnimalListByAnimal(int idAnimal, string dbName);
        Task<DataResultDTO<BreedAnimalResponse>> GetBreedAnimalList(NameBaseEntityFilter filter, string dbName);
    }
}

using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IOwnersRepository : IGenericRepository<Owners, OwnersFilter>
	{
		Task<Owners> ReadByPatientIdAsync(int id, string dbName);
	}
}

using Domain.Entities.Models.Masters;

namespace Domain.Interfaces.Masters
{
    public interface IPlaceRepository
    {
        Task<IEnumerable<Provinces>> GetProvinces();
        Task<IEnumerable<States>> GetStateProvinces(int provinceId);
    }
}

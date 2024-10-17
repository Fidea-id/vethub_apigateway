using Domain.Entities.Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Masters
{
    public interface IPlaceRepository
    {
        Task<IEnumerable<Provinces>> GetProvinces();
        Task<IEnumerable<States>> GetStateProvinces(int provinceId);
    }
}

using Domain.Entities.DTOs.Clients;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clients
{
    public interface IPatientsStatisticRepository : IGenericRepository<PatientsStatistic, PatientsStatisticFilter>
    {
        Task<IEnumerable<PatientsStatisticDto>> ReadPatientsStatisticAsync(int patientId, string dbName);
    }
}

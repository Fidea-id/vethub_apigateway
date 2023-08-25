using Domain.Entities.Filters;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clients
{
    public interface IPrescriptionFrequentsRepository : IGenericRepository<PrescriptionFrequents, PrescriptionFrequentsFilter>
    {
    }
}

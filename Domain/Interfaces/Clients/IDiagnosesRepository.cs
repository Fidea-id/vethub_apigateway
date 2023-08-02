using Domain.Entities.Filters;
using Domain.Entities.Models.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clients
{
    public interface IDiagnosesRepository : IGenericRepository<Diagnoses, NameBaseEntityFilter>
    {
    }
}

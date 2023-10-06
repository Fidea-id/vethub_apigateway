using Domain.Entities.Filters;
using Domain.Entities.Filters.Masters;
using Domain.Entities.Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Masters
{
    public interface IUserDemoRepository : IGenericRepository<UserDemo, NameBaseEntityFilter>
    {
    }
}

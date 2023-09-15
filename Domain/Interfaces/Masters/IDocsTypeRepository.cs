using Domain.Entities.Filters;
using Domain.Entities.Models.Masters;

namespace Domain.Interfaces.Masters
{
    public interface IDocsTypeRepository : IGenericRepository<DocsType, NameBaseEntityFilter>
    {
    }
}

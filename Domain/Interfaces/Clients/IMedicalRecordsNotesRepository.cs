using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IMedicalRecordsNotesRepository : IGenericRepository<MedicalRecordsNotes, MedicalRecordsNotesFilter>
    {
    }
}

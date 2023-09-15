using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses.Clients;

namespace Application.Services.Contracts
{
    public interface IDocGenerateService
    {
        Task<DocGenerateResponse> GenerateSuratKematianAsync(string userId, DocsKematianRequest request, string auth);
        Task<DocGenerateResponse> GenerateSuratPermintaanPulangAsync(string userId, DocsPermintaanPulangRequest request, string auth);
        Task<DocGenerateResponse> GenerateSuratTindakanAsync(string userId, DocsTindakanRequest request, string auth);
        Task<DocGenerateResponse> GenerateSuratRujukanAsync(string userId, DocsRujukanRequest request, string auth);
    }
}

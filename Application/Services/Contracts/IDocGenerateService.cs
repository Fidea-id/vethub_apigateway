using Domain.Entities.DTOs.Clients;
using Domain.Entities.Responses.Clients;

namespace Application.Services.Contracts
{
    public interface IDocGenerateService
    {
        Task<DocGenerateResponse> GenerateSuratKematianAsync(SuratKematianData data);
    }
}

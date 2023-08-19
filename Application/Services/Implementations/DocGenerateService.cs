using Application.Services.Contracts;
using Domain.Entities.DTOs.Clients;
using Domain.Entities.Responses.Clients;

namespace Application.Services.Implementations
{
    public class DocGenerateService : IDocGenerateService
    {
        public Task<DocGenerateResponse> GenerateSuratKematianAsync(SuratKematianData data)
        {
            throw new NotImplementedException();
        }
    }
}

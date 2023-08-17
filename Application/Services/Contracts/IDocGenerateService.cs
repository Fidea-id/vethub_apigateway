using Domain.Entities.DTOs.Clients;
using Domain.Entities.Responses.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Contracts
{
    public interface IDocGenerateService
    {
        Task<DocGenerateResponse> GenerateSuratKematianAsync(SuratKematianData data);
    }
}

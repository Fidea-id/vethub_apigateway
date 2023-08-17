using Domain.Entities;

namespace Application.Services.Contracts
{
    public interface IUriService
    {
        Uri GetAPIUri(APIType type, string query = null);
        Uri GetBaseUri(string query = null);
        Uri GetBaseWebUri(string query = null);
    }
}

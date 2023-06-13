using Domain.Entities;

namespace Application.Services.Contracts
{
    public interface IUriService
    {
        Uri GetMasterAPIUri(string query = null);
        Uri GetClientAPIUri(string query = null);
        Uri GetAPIUri(APIType type, string query = null);
    }
}

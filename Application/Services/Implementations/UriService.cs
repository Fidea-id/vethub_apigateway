using Application.Services.Contracts;
using Domain.Entities;

namespace Application.Services.Implementations
{
    public class UriService : IUriService
    {
        private readonly BaseURI _baseUri;
        public UriService(BaseURI baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetAPIUri(APIType type, string query = null)
        {
            var stringURI = "";
            if (type == APIType.Master)
            {
                stringURI = _baseUri.MasterAPIURI;
            }
            else
            {
                stringURI = _baseUri.ClientAPIURI;
            }
            var uri = new Uri(stringURI);
            if (query == null) return uri;

            var modifiedUri = stringURI + "" + query;
            return new Uri(modifiedUri);
        }

        public Uri GetBaseUri(string query = null)
        {
            var stringURI = _baseUri.APIURI;
            var uri = new Uri(stringURI);
            if (query == null) return uri;

            var modifiedUri = stringURI + "" + query;
            return new Uri(modifiedUri);
        }
        public Uri GetBaseWebUri(string query = null)
        {
            var stringURI = _baseUri.APIWEBURI;
            var uri = new Uri(stringURI);
            if (query == null) return uri;

            var modifiedUri = stringURI + "" + query;
            return new Uri(modifiedUri);
        }
    }
}

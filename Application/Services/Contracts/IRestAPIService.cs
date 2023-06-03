using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Contracts
{
    public interface IRestAPIService
    {
        Task<T> GetResponse<T>(string type, string url, string auth = null) 
            where T:class;
        Task<T> PostResponse<T>(string type, string url, string obj, string auth = null)
            where T : class;
    }
}

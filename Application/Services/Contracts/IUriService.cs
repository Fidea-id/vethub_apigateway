using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Contracts
{
    public interface IUriService
    {
        Uri GetMasterAPIUri(string query = null);
        Uri GetClientAPIUri(string query = null);
    }
}

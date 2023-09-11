using Domain.Entities.Filters;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clients
{
    public interface IProductStockRepository : IGenericRepository<ProductStocks, NameBaseEntityFilter>
    {
        Task UpdateMinStock(int productId, int quantity, string dbName);
        Task UpdateAddStock(int productId, int quantity, string dbName);
    }
}

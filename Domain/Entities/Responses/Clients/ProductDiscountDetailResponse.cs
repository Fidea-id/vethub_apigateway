using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses.Clients
{
    public class ProductDiscountDetailResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int DiscountValue { get; set; }
        public string DiscountType { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
    }
}

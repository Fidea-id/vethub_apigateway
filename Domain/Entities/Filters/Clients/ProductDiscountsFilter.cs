using Domain.Entities.Models.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Filters.Clients
{
    public class ProductDiscountsFilter: BaseEntityFilter
    {
        public int? ProductId { get; set; }
        public string? Description { get; set; }
        public double? DiscountValue { get; set; }
        public DiscountType? DiscountType { get; set; } // Enum or separate table to indicate the type of discount
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

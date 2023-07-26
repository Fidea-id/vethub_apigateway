using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses.Clients
{
    public class ProductBundleDetailResponse
    {
        public int BundleId { get; set; }
        public string BundleName { get; set; }
        public double BundlePrice { get; set; }
        public int BundleStock { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public double ItemQuantity { get; set; }
        public double ItemPrice { get; set; }
        public int ItemStock { get; set; }
        public bool IsStockAvailable
        {
            get { return ItemStock > 0; }
        }
    }
}

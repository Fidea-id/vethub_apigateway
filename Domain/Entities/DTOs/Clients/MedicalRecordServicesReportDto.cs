using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTOs.Clients
{
    public class MedicalRecordServicesReportDto
    {
        public string MedicalRecordCode { get; set; }
        public DateTime Date { get; set; }
        public string StaffName { get; set; }
        public string ServiceName { get; set; }
        public double ServiceBasePrice { get; set; }
        public double ServiceActualPrice { get; set; }
        public double PriceDifference { get; set; }
        public bool IsPriceModified { get; set; }
        public double PrescriptionAmount { get; set; }
        public double Quantity { get; set; }
        public double Total { get; set; }
        public string PrescriptionFrequency { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? EndDate { get; set; }
        public int ServiceDuration { get; set; }
        public string ServiceDurationType { get; set; }
    }
}

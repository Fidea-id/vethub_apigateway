using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses.Clients
{
    public class InvoiceResponse
    {
        public MedicalRecordsDetailResponse Detail { get; set; }
        public Clinics ClinicData { get; set; }
        public IEnumerable<OrdersPayment> PaymentData { get; set; }
        public IEnumerable<PaymentMethod> PaymentMethodData { get; set; }
}
}

using Domain.Entities.Models.Clients;

namespace Domain.Entities.Responses.Clients
{
    public class InvoiceResponse
    {
        public MedicalRecordsDetailResponse Detail { get; set; }
        public OrderFullResponse DetailOrder { get; set; }
        public Clinics ClinicData { get; set; }
        public IEnumerable<OrdersPayment> PaymentData { get; set; }
        public IEnumerable<OrdersPaymentResponse> PaymentDataResponse { get; set; }
        public IEnumerable<PaymentMethod> PaymentMethodData { get; set; }
    }
}

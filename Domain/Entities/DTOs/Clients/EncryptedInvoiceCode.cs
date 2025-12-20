namespace Domain.Entities.DTOs.Clients
{
    public class EncryptedInvoiceCode
    {
        public string Entity { get; set; }
        public string Id { get; set; }
    }
    public class DecryptedInvoiceCode
    {
        public string Entity { get; set; }
        public int Id { get; set; }
    }
}

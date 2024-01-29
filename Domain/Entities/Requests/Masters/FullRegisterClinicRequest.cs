using Domain.Entities.Requests.Clients;

namespace Domain.Entities.Requests.Masters
{
    public class OwnerRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Photo { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class FullRegisterClinicRequest
    {
        public ClinicsRequest ClinicData { get; set; }
        public int SubscriptionId { get; set; }
        public int MaxUsers { get; set; }
        public OwnerRequest OwnerData { get; set; }
        public IEnumerable<StaffRequest>? StaffData { get; set; }
    }
}

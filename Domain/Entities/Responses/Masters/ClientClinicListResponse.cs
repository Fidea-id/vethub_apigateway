namespace Domain.Entities.Responses.Masters
{
    public class ClientClinicListResponse
    {
        public int Id { get; set; } // clinics id
        public ClientClinicResponse ClinicData { get; set; }
        public ClientOwnerResponse OwnerData { get; set; }
        public DateTime JoinDate { get; set; }
        public string Status { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class ClientClinicDetailResponse
    {
        public int Id { get; set; } // clinics id
        public ClientClinicResponse ClinicData { get; set; }
        public ClientOwnerResponse OwnerData { get; set; }
        public UserBillResponse LatestBillData { get; set; }
        public DateTime JoinDate { get; set; }
    }
    public class ClientOwnerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class ClientClinicResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        //public string Entity { get; set; }
        public string WebUrl { get; set; }
        public string MapUrl { get; set; }
    }
}

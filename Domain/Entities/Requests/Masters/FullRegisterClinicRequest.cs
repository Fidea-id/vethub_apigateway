namespace Domain.Entities.Requests.Masters
{
    public class FullRegisterClinicRequest
    {
        public string ClinicName { get; set; }
        public string ClinicsAddress { get; set; }
        public string ClinicsCity { get; set; }
        public string ClinicsState { get; set; }
        public string? ClinicsDescription { get; set; }
        public string ClinicsLogo { get; set; }
        public string ClinicsPhoneNumber { get; set; }
        public string ClinicsEmail { get; set; }
        public string ClinicsWebUrl { get; set; }
        public string ClinicsMapUrl { get; set; }
        public string OwnersName { get; set; }
        public string OwnersEmail { get; set; }
        public string? OwnersPhoto { get; set; }
        public string? OwnersPhoneNumber { get; set; }
    }
}

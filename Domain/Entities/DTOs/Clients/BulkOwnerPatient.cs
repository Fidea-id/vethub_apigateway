namespace Domain.Entities.DTOs.Clients
{
    public class BulkOwnerPatients
    {
        public IEnumerable<BulkOwnerPatient> listData { get; set; }
    }
    public class BulkOwnerPatient
    {
        public int row { get; set; }
        public string? ownerTitle { get; set; }
        public string ownerName { get; set; }
        public string? ownerEmail { get; set; }
        public string ownerPhone { get; set; }
        public string? ownerAddress { get; set; }
        public string patientName { get; set; }
        public string patientSpecies { get; set; }
        public string patientBreed { get; set; }
        public string patientGender { get; set; }
        public string? patienColor { get; set; }
        public string? patienDOB { get; set; }
        public bool? isAlive { get; set; }
        public bool? isVaccinated { get; set; }
    }
}

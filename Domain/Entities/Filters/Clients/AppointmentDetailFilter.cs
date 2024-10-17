namespace Domain.Entities.Filters.Clients
{
    public class AppointmentDetailFilter : BaseEntityFilter
    {
        public int? StatusId { get; set; }
        //public int? BranchId { get; set; }
        public int? StaffId { get; set; }
        public string? Date { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}

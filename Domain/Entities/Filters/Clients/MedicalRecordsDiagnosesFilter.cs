﻿namespace Domain.Entities.Filters.Clients
{
    public class MedicalRecordsDiagnosesFilter : BaseEntityFilter
    {
        public int? MedicalRecordsId { get; set; }
        public string? Diagnose { get; set; }
        public string? Prognose { get; set; }
        //public double? TotalPrice { get; set; }
    }
}

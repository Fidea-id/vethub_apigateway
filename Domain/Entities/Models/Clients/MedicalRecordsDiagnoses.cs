﻿namespace Domain.Entities.Models.Clients
{
    public class MedicalRecordsDiagnoses : BaseEntity
    {
        public int MedicalRecordsId { get; set; }
        public string Diagnose { get; set; }
    }
}

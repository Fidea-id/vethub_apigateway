﻿namespace Domain.Entities.Responses.Clients
{
    public class MedicalRecordsNotesResponse
    {
        public int Id { get; set; }
        public int MedicalRecordsId { get; set; }
        public int StaffId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}

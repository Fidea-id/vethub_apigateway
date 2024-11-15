﻿namespace Domain.Entities.Requests.Clients
{
    public class MedicalRecordsPrescriptionsRequest
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PrescriptionFrequency { get; set; }
        public string Type { get; set; }
        public double PrescriptionAmount { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double Total { get; set; }
    }
}

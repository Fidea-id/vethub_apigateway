using System;
using System.Collections.Generic;

namespace Domain.Entities.Responses.Clients
{
    public class PharmacyMedicalRecordDetailResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public PharmacyAppointmentSummaryResponse Appointments { get; set; }
        public PharmacyServiceSummaryResponse Services { get; set; }
        public PharmacyPatientSummaryResponse Patients { get; set; }
        public PharmacyOwnerSummaryResponse Owners { get; set; }
        public PharmacyStaffSummaryResponse Staff { get; set; }
        public IEnumerable<PharmacyPrescriptionItemResponse> Prescriptions { get; set; }
    }

    public class PharmacyAppointmentSummaryResponse
    {
        public int Id { get; set; }
    }

    public class PharmacyServiceSummaryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public class PharmacyPatientSummaryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string Color { get; set; }
    }

    public class PharmacyOwnerSummaryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public class PharmacyStaffSummaryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PharmacyPrescriptionItemResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PrescriptionFrequency { get; set; }
        public string Type { get; set; }
        public string? MixId { get; set; }
        public string? MixName { get; set; }
        public double PrescriptionAmount { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double Total { get; set; }
        public string? ProductVolumeUnit { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses.Clients
{
    public class AppointmentMedicalDetailResponse
    {
        public int AppointmentId { get; set; }
        public int MedicalRecordId { get; set; }
        public int OwnersId { get; set; }
        public string OwnersName { get; set; }
        public string OwnersTitle { get; set; }
        public int PatientsId { get; set; }
        public string PatientsName { get; set; }
        public string PatientsBreed { get; set; }
        public DateTime Date { get; set; }
        public int? DurationEstimate { get; set; }
        public string? DurationTypeEstimate { get; set; }
        public DateTime? EndDateEstimate { get; set; }
        public int? DurationActual { get; set; }
        public string? DurationTypeActual { get; set; }
        public DateTime? EndDateActual { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Notes { get; set; }
        public double Total { get; set; }
        public bool IsOpname { get; set; }
        public string Diagnoses { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Prescriptions { get; set; }
        public string Services { get; set; }
        public string StatusPayment { get; set; }
    }
}

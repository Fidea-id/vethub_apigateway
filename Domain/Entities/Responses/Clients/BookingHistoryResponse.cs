using Domain.Entities.Models.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Responses.Clients
{
    public class BookingHistoryResponse
    {
        public int MedicalRecordsId { get; set; }
        public int AppointmentId { get; set; }
        public string Code { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerTitle { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientSpecies { get; set; }
        public string PatientBreed { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public DateTime DateAppointment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPrice { get; set; }
        public IEnumerable<MedicalRecordsDiagnoses>? Diagnoses { get; set; }
        public string StatusName { get; set; }
        public string StatusPayment { get; set; }
    }
}

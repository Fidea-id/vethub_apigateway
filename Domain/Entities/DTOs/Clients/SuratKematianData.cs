using Domain.Entities.Models.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTOs.Clients
{
    public class SuratKematianData
    {
        public Clinics ClinicData { get; set; }
        public Patients PatientData { get; set; }
        public Owners OwnerData { get; set; }
        public MedicalRecords MedicalData { get; set; }
    }
}

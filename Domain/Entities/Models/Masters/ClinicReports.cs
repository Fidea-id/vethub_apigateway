using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.Masters
{
    public class ClinicReports: BaseEntity
    {
        public int? ClinicId { get; set; } //ambil dari tabel Clinics.Id di master (jika ada)
        public int? OwnerId { get; set; } //ambil dari tabel User.Id di master (roleOwner, jika ada dobel, sandingkan dengan BillPayment.UserId, harusnya hanya ada 1 owner yg terdaftar di BillPayment)
        public string Entity { get; set; }
        public string? ClinicName { get; set; }
        public string? SubscriptionName { get; set; } //ambil dari tabel Subscription.Name di master (ambil dulu dari BillPayments)
        public int? TotalPatients { get; set; }
        public int? TotalAppointments { get; set; }
        public int? TotalMedicalRecords { get; set; }
        public double? TotalRevenue { get; set; }
        public DateTime? LastActivity { get; set; }
        public DateTime? SubscriptionEndDate { get; set; } //ambil dari tabel BillPayments.EndDate di master (ambil dari ID User Owner)
        public DateTime? JoinDate { get; set; } //ambil dari tabel User.CreatedAt di master
        public DateTime? CreatedAt { get; set; } //ambil dari tabel User.CreatedAt di master
    }
}

﻿using Domain.Entities.DTOs;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Responses.Clients;

namespace Domain.Interfaces.Clients
{
    public interface IAppointmentRepository : IGenericRepository<Appointments, AppointmentsFilter>
    {
        //Task<IEnumerable<PatientsListResponse>> GetPatientsList(string dbName, AppointmentsFilter filter);
        Task<IEnumerable<BookingHistoryResponse>> GetBookingHistoryOwner(string dbName, int ownerId);
        Task<IEnumerable<BookingHistoryResponse>> GetBookingHistoryPatient(string dbName, int patientId);
        Task AddStatusRange(IEnumerable<AppointmentsStatus> entities, string dbName);
        Task<int> AddActivity(AppointmentsActivity entities, string dbName);
        Task<IEnumerable<AppointmentsStatus>> GetAllStatus(string dbName);
        Task<DataResultDTO<AppointmentsDetailResponse>> GetAllDetailList(string dbName, AppointmentDetailFilter filter);
        Task<DataResultDTO<AppointmentMedicalDetailResponse>> GetAllDetailMedicalList(string dbName, AppointmentDetailFilter filter);
        Task<IEnumerable<Appointments>> GetAllByStatusId(string dbName, int statusId);
        Task<IEnumerable<AppointmentsDetailResponse>> GetAllDetailListToday(string dbName);
        Task<AppointmentsDetailResponse> GetAllDetail(int id, string dbName);
    }
}

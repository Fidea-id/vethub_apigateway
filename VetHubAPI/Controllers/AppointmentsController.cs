﻿using Application.Services.Contracts;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Entities.Filters.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Models.Masters;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses;
using Domain.Entities.Responses.Clients;
using Domain.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppointmentsController : Controller
    {
        private readonly IRestAPIService _restAPIService;
        public AppointmentsController(IRestAPIService restAPIService)
        {
            _restAPIService = restAPIService;
        }

        [HttpGet]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetAppointment([FromQuery] AppointmentsFilter filter)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<Appointments>, AppointmentsFilter>(APIType.Client, "Appointments", authToken, filter);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<Appointments>(APIType.Client, $"Appointments/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Detail")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetAppointmentDetail([FromQuery] AppointmentDetailFilter filter)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                bool isCalendar = false;
                if (filter.Start.HasValue && filter.End.HasValue)
                {
                    isCalendar = true;
                    filter.Date = FormatUtil.GetMonthStartEndString(filter.Start.Value, filter.End.Value);
                }
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<AppointmentsDetailResponse>, AppointmentDetailFilter>(APIType.Client, "Appointments/Detail", authToken, filter);
                var result = response.Data;
                if (result.Count() > 0)
                {
                    if (!isCalendar) // if calendar true, then dont need to show the medical detail
                    {
                        foreach (var item in result)
                        {
                            if (item.MedicalRecordId != null || item.MedicalRecordId != 0)
                            {
                                try
                                {
                                    var responseDetail = await _restAPIService.GetResponse<MedicalRecordsDetailResponse>(APIType.Client, $"MedicalRecords/Detail/{item.MedicalRecordId}?flag=no_notes", authToken);
                                    item.MedicalRecord = responseDetail;
                                }
                                catch
                                {
                                    item.MedicalRecord = null;
                                }
                            }
                        }
                    }
                }

                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("InvoiceEmail/{id}")]
        public async Task<IActionResult> PostInvoiceEmail(int id)
        {
            try
            {
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<BaseAPIResponse>(APIType.Client, "Appointments/InvoiceEmail/" + id, authToken);

                return Ok();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Detail/Medical")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetAppointmentDetailMedical([FromQuery] AppointmentDetailFilter filter)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponseFilter<DataResultDTO<AppointmentMedicalDetailResponse>, AppointmentDetailFilter>(APIType.Client, "Appointments/Detail/Medical", authToken, filter);
                var result = response.Data.ToList();
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        if (item.MedicalRecordId != null || item.MedicalRecordId != 0)
                        {
                            try
                            {
                                var responseDetail = await _restAPIService.GetResponse<MedicalRecordsMinResponse>(APIType.Client, $"MedicalRecords/DetailMin/{item.MedicalRecordId}", authToken);
                                item.Code = responseDetail.Code;
                                item.Diagnoses = responseDetail.Diagnoses;
                                item.Prescriptions = responseDetail.Prescriptions;
                                item.Services = responseDetail.Services;
                                item.StatusPayment = responseDetail.StatusPayment;
                            }
                            catch
                            {
                                item.Code = null;
                                item.Diagnoses = null;
                                item.Prescriptions = null;
                                item.Services = null;
                                item.StatusPayment = null;
                            }
                        }
                    }
                    // Remove items where Diagnoses is null
                    result.RemoveAll(item => item.Diagnoses == null || item.Diagnoses == "");
                }

                return ResponseUtil.CustomOk(result, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Detail/Owner/{ownerId}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetAppointmentDetailOwner(int ownerId)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<DataResultDTO<BookingHistoryResponse>>(APIType.Client, "MedicalRecords/Detail/Owner/" + ownerId, authToken);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Detail/History/{medId}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetMedicalDetailHistory(int medId)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<DataResultDTO<MedicalRecordsHistoryResponse>>(APIType.Client, "MedicalRecords/Detail/History/" + medId, authToken);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Detail/Patient/{patientId}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetAppointmentDetailPatient(int patientId)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<DataResultDTO<BookingHistoryResponse>>(APIType.Client, "MedicalRecords/Detail/Patient/" + patientId, authToken);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Detail/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetAppointmentDetailById(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<AppointmentsDetailResponse>(APIType.Client, $"Appointments/Detail/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        [HttpGet("MedicalRecords/Detail/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetMedicalRecordDetailById(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<MedicalRecordsDetailResponse>(APIType.Client, $"MedicalRecords/Detail/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        [HttpGet("MedicalRecords/Invoice/{medicalId}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetMedicalRecordDetailInvoice(int medicalId)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<InvoiceResponse>(APIType.Client, $"Appointments/Invoice/{medicalId}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [AllowAnonymous]
        [HttpGet("Public/Invoice")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetPublicInvoice(string e, string d)
        {
            try
            {
                //var dbName = EncryptionHelper.DecryptString(e);
                //var idString = EncryptionHelper.DecryptString(d);
                var response = await _restAPIService.GetResponse<InvoiceResponse>(APIType.Client, $"Appointments/PublicInvoice?e={e}&d={d}");
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("MedicalRecords/Payment/{medicalId}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetMedicalRecordPayment(int medicalId)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<OrdersPayment>>(APIType.Client, $"MedicalRecords/Payment/{medicalId}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("today")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetTodayAppointment()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<AppointmentsDetailResponse>>(APIType.Client, $"Appointments/Today", authToken);
                return ResponseUtil.CustomOk(response, 200, response.Count());
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("status")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetAppointmentStatus()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<AppointmentsStatus>>(APIType.Client, "Appointments/Status", authToken);
                return ResponseUtil.CustomOk(response, 200, response.Count());
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("status/{id}")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetAppointmentStatusById(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<AppointmentsStatus>(APIType.Client, $"Appointments/status/{id}", authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAppointments([FromBody] AppointmentsRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<AppointmentsDetailResponse>(APIType.Client, "Appointments", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Reschedule/{id}")]
        public async Task<IActionResult> PostAppointments(int id, [FromBody] AppointmentsRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<Appointments>(APIType.Client, "Appointments", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Payment")]
        public async Task<IActionResult> PostAppointmentPayment([FromBody] OrdersPaymentRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<OrdersPaymentResponse>(APIType.Client, "MedicalRecords/Payment", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("StatusChange")]
        public async Task<IActionResult> StatusChange([FromBody] AppointmentsRequestChangeStatus request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<AppointmentsDetailResponse>(APIType.Client, "Appointments/StatusChange", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("MedicalNotes/{medicalRecordId}")]
        public async Task<IActionResult> MedicalCheckupNoteGet(int medicalRecordId)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<IEnumerable<MedicalRecordsNotes>>(APIType.Client, "MedicalRecords/Notes/" + medicalRecordId, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("MedicalNotes")]
        public async Task<IActionResult> MedicalCheckupNotePost([FromBody] MedicalRecordsNotesRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.PostResponse<MedicalRecordsNotesResponse>(APIType.Client, "MedicalRecords/Notes/", JsonConvert.SerializeObject(request), authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("MedicalNotes/{id}")]
        public async Task<IActionResult> MedicalCheckupNotePut(int id, [FromBody] MedicalRecordsNotesRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.PutResponse<MedicalRecordsNotesResponse>(APIType.Client, "MedicalRecords/Notes", id, JsonConvert.SerializeObject(request), authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> AppointmentsDelete(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<Appointments>(APIType.Client, "Appointments", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("MedicalNotes/{id}")]
        public async Task<IActionResult> MedicalCheckupNoteDelete(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<MedicalRecordsNotes>(APIType.Client, "MedicalRecords/Notes", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("CheckupDone")]
        public async Task<IActionResult> MedicalCheckupDone([FromBody] MedicalRecordsDetailRequest request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<MedicalRecordsDetailResponse>(APIType.Client, "MedicalRecords/Detail", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("Prescription/{id}")]
        public async Task<IActionResult> EditPrescription(int id, [FromBody] IEnumerable<MedicalRecordsPrescriptionsRequest> request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PutResponse<IEnumerable<MedicalRecordsDetailResponse>>(APIType.Client, "MedicalRecords/Prescription", id, requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Type")]
        [ResponseCache(Duration = 60)] // Cache response for 60 seconds
        public async Task<IActionResult> GetAppointmentType()
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.GetResponse<DataResultDTO<AppointmentsType>>(APIType.Client, "Appointments/Type", authToken);
                return ResponseUtil.CustomOk(response.Data, 200, response.TotalData);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Type")]
        public async Task<IActionResult> PostAppointmentsType([FromBody] AppointmentsType request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var requestJson = JsonConvert.SerializeObject(request);
                var response = await _restAPIService.PostResponse<AppointmentsType>(APIType.Client, "Appointments/Type", requestJson, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("Type/{id}")]
        public async Task<IActionResult> PutAppointmentsType(int id, [FromBody] AppointmentsType request)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.PutResponse<AppointmentsType>(APIType.Client, "Appointments/Type", id, JsonConvert.SerializeObject(request), authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
        
        [HttpDelete("Type/{id}")]
        public async Task<IActionResult> DeleteAppointmentsType(int id)
        {
            try
            {
                //Get the AuthToken
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var response = await _restAPIService.DeleteResponse<AppointmentsType>(APIType.Client, "Appointments/Type", id, authToken);
                return ResponseUtil.CustomOk(response, 200);
            }
            catch
            {
                throw;
            }
        }
    }
}

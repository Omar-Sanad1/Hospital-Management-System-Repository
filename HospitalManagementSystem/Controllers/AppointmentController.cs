using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Filtering;
using Core.Interfaces;
using Core.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models.AppointmentModels;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("GetAllAppointmentsPagedFiltered")]
        public async Task<IActionResult> GetAllAppointmentsPagedFilteredAsync([FromQuery] AppointmentFiltering appointmentFiltering, [FromQuery] PaginationParameters paginationParameters)
        {
            var appointments = _appointmentService.GetAllAppointmentsFiltered(a =>
            // فلترة ب AppointmentDate
            (!appointmentFiltering.AppointmentDate.HasValue || a.AppointmentDate == appointmentFiltering.AppointmentDate.Value) &&
            // فلترة ب AppointmentTime
            (!appointmentFiltering.AppointmentTime.HasValue || a.AppointmentTime == appointmentFiltering.AppointmentTime.Value) &&
            // فلترة ب PatientID
            (!appointmentFiltering.PatientID.HasValue || a.PatientID == appointmentFiltering.PatientID) &&
            // فلترة ب DoctorID
            (!appointmentFiltering.DoctorID.HasValue || a.DoctorID == appointmentFiltering.DoctorID)
            );

            // Sorting
            appointments = appointmentFiltering.SortBy?.ToLower() switch
            {
                "appointmentdate" => appointmentFiltering.isDescending
                ? appointments.OrderByDescending(a => a.AppointmentDate)
                : appointments.OrderBy(a => a.AppointmentDate),

                "appointmenttype" => appointmentFiltering.isDescending
                ? appointments.OrderByDescending(a => a.AppointmentType)
                : appointments.OrderBy(a => a.AppointmentType),

                "bookingdate" => appointmentFiltering.isDescending
                ? appointments.OrderByDescending(a => a.BookingDate)
                : appointments.OrderBy(a => a.BookingDate),

                _ => appointments.OrderBy(a => a.ID)
            };

            // Pagination
            var totalAppointments = appointments.Count();
            var appointmentsPaged = await _appointmentService.GetAllAppointmentsPagedAsync(paginationParameters.PageNumber, paginationParameters.PageSize);

            var result = new PaginationResponse<AppointmentToReturnDTO>
            (
                data: appointments,
                totalitems: totalAppointments,
                pageNumber: paginationParameters.PageNumber,
                pageSize: paginationParameters.PageSize
            );

            return Ok(result);
        }


        [HttpGet("GetAppointmentByID{id}")]
        public async Task<IActionResult> GetAppointmentByIDAsync(int appointmentId)
        {
            var appointment = await _appointmentService.GetAppointmentByIDAsync(appointmentId);
            return Ok(appointment);
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost("CreateNewAppointment")]
        public async Task<IActionResult> CreateNewAppointmentAsync(CreateNewAppointmentModel createNewAppointment)
        {
            var createdAppointment = await _appointmentService.CreateNewAppointmentAsync(createNewAppointment);
            return Ok(createdAppointment);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("UpdateAppointmentInformation")]
        public async Task<IActionResult> UpdateAppointmentInformationAsync(int appointmentId, UpdateAppointmentInformationModel updateAppointmentInformation)
        {
            var updatedAppointment = await _appointmentService.UpdateAppointmentInformationAsync(appointmentId, updateAppointmentInformation);
            return Ok(updatedAppointment);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("DeleteAppointment")]
        public async Task DeleteAppointmentAsync(int appointmentId)
        {
            await _appointmentService.DeleteAppointmentAsync(appointmentId);
        }
    }
}

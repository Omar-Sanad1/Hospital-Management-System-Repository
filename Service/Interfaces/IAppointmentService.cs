using Core.DTOs;
using Core.Entities;
using Core.Filtering;
using Service.Models.AppointmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAppointmentService
    {
        public Task<IEnumerable<AppointmentToReturnDTO>> GetAllAppointmentsAsync();
        public Task<IEnumerable<AppointmentToReturnDTO>> GetAllAppointmentsPagedAsync(int pageNumber , int pageSize);
        public IEnumerable<AppointmentToReturnDTO> GetAllAppointmentsFiltered(Func<Appointment, bool> Filter);
        public Task<AppointmentToReturnDTO> GetAppointmentByIDAsync(int appointmentId);
        public Task<AppointmentToReturnDTO> CreateNewAppointmentAsync(CreateNewAppointmentModel createNewAppointment);
        public Task<AppointmentToReturnDTO> UpdateAppointmentInformationAsync(int appointmentId, UpdateAppointmentInformationModel updateAppointmentInformation);
        public Task DeleteAppointmentAsync(int appointmentId);
    }
}

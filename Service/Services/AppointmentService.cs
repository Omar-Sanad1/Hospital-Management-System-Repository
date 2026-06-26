using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Filtering;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Service.Interfaces;
using Service.Models.AppointmentModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HospitalManagementSystemDbContext _dbContext;
        private readonly IMapper _mapper;
        public AppointmentService(HospitalManagementSystemDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AppointmentToReturnDTO>> GetAllAppointmentsAsync()
        {
            var appointments = await _dbContext.Appointments.ToListAsync();

            return _mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentToReturnDTO>>(appointments);
        }

        public IEnumerable<AppointmentToReturnDTO> GetAllAppointmentsFiltered(Func<Appointment,bool> Filter)
        {
            var appointmentsFiltered = _dbContext.Set<Appointment>()
                                       .Where(Filter)
                                       .ToList();

            return _mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentToReturnDTO>>(appointmentsFiltered);
        }

        public async Task<IEnumerable<AppointmentToReturnDTO>> GetAllAppointmentsPagedAsync(int pageNumber, int pageSize)
        {
            var appointmentsPaged = await _dbContext.Set<Appointment>()
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return _mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentToReturnDTO>>(appointmentsPaged);

        }


        public async Task<AppointmentToReturnDTO> GetAppointmentByIDAsync(int appointmentId)
        {
            var specifiedAppointment = await _dbContext.Appointments.FirstOrDefaultAsync(a=>a.ID == appointmentId);
            if (specifiedAppointment is null)
                throw new Exception("This appointment isn't exist");

            return _mapper.Map<Appointment, AppointmentToReturnDTO>(specifiedAppointment);
        }


        public async Task<AppointmentToReturnDTO> CreateNewAppointmentAsync(CreateNewAppointmentModel createNewAppointment)
        {
            var existsDoctor = await _dbContext.Doctors.FindAsync(createNewAppointment.DoctorID);
            if (existsDoctor is null)
                throw new Exception("This doctor id isn't available");

            var existsPatient = await _dbContext.Patients.FindAsync(createNewAppointment.PatientID);
            if (existsPatient is null)
                throw new Exception("This patient id isn't available");

            if(createNewAppointment.AppointmentDate < DateOnly.FromDateTime(DateTime.Today))
                throw new Exception("Appointment date can't be in the past");

            var appointment = new Appointment
            {
                AppointmentDate = createNewAppointment.AppointmentDate,
                AppointmentTime = createNewAppointment.AppointmentTime,
                AppointmentType = createNewAppointment.AppointmentType,
                ReasonForVisit = createNewAppointment.ReasonForVisit,
                BookingDate = createNewAppointment.BookingDate,
                PatientID = createNewAppointment.PatientID,
                DoctorID = createNewAppointment.DoctorID
            };

            await _dbContext.Appointments.AddAsync(appointment);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Appointment, AppointmentToReturnDTO>(appointment);
        }


        public async Task DeleteAppointmentAsync(int appointmentId)
        {
            var specifiedAppointment = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.ID == appointmentId);
            if (specifiedAppointment is null)
                throw new Exception("This appointment isn't exist");

            _dbContext.Appointments.Remove(specifiedAppointment);
        }


        public async Task<AppointmentToReturnDTO> UpdateAppointmentInformationAsync(int appointmentId, UpdateAppointmentInformationModel updateAppointmentInformation)
        {
            var specifiedAppointment = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.ID == appointmentId);
            if (specifiedAppointment is null)
                throw new Exception("This appointment isn't exist");

            specifiedAppointment.AppointmentDate = updateAppointmentInformation.AppointmentDate;
            specifiedAppointment.AppointmentTime = updateAppointmentInformation.AppointmentTime;
            specifiedAppointment.AppointmentType = updateAppointmentInformation.AppointmentType;
            specifiedAppointment.ReasonForVisit = updateAppointmentInformation.ReasonForVisit;
            specifiedAppointment.BookingDate = updateAppointmentInformation.BookingDate;
            specifiedAppointment.PatientID = updateAppointmentInformation.PatientID;
            specifiedAppointment.DoctorID = updateAppointmentInformation.DoctorID;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Appointment, AppointmentToReturnDTO>(specifiedAppointment);
        }
    }
}

using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace HospitalManagementSystem.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Appointment, AppointmentToReturnDTO>()
                .ForMember(a => a.PatientName, a => a.MapFrom(a => a.Patient.FullName))
                .ForMember(a => a.DoctorName, a => a.MapFrom(a => a.Doctor.FullName));
            //////////////////////////////////////////////////////////////////////////////////
            CreateMap<Bill, BillToReturnDTO>()
                .ForMember(b => b.PatientName, b => b.MapFrom(b => b.Patient.FullName));
            //////////////////////////////////////////////////////////////////////////////////
            CreateMap<Department, DepartmentToReturnDTO>();
            //////////////////////////////////////////////////////////////////////////////////
            CreateMap<Doctor, DoctorToReturnDTO>();
            //////////////////////////////////////////////////////////////////////////////////
            CreateMap<LaboratoryTest, LaboratoryTestToReturnDTO>()
                .ForMember(l => l.PatientName, l => l.MapFrom(l => l.Patient.FullName))
                .ForMember(l => l.DoctorName, l => l.MapFrom(l => l.Doctor.FullName));
            //////////////////////////////////////////////////////////////////////////////////
            CreateMap<MedicalRecord, MedicalRecordToReturnDTO>()
               .ForMember(m => m.PatientName, m => m.MapFrom(m => m.Patient.FullName))
               .ForMember(m => m.DoctorName, m => m.MapFrom(m => m.Doctor.FullName));
            //////////////////////////////////////////////////////////////////////////////////
            CreateMap<Patient, PatientToReturnDTO>();
            //////////////////////////////////////////////////////////////////////////////////
            CreateMap<Payment, PaymentToReturnDTO>();
            //////////////////////////////////////////////////////////////////////////////////
            CreateMap<Prescription, PrescriptionToReturnDTO>()
              .ForMember(p => p.PatientName, p => p.MapFrom(p => p.Patient.FullName))
              .ForMember(p => p.DoctorName, p => p.MapFrom(p => p.Doctor.FullName));
            //////////////////////////////////////////////////////////////////////////////////
            CreateMap<Role, RoleToReturnDTO>();
            //////////////////////////////////////////////////////////////////////////////////
            CreateMap<User,UserToReturnDTO>();

        }
    }
}

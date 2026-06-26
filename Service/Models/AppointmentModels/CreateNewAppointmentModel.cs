using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.AppointmentModels
{
    public class CreateNewAppointmentModel
    {
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public string AppointmentType { get; set; }
        public string ReasonForVisit { get; set; }
        public DateTime BookingDate { get; set; }
        public int PatientID { get; set; } 
        public int DoctorID { get; set; } 
    }
}

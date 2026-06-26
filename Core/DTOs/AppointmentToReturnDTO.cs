using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class AppointmentToReturnDTO
    {
        public int ID { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public string AppointmentType { get; set; }
        public DateTime BookingDate { get; set; }
        public string PatientName { get; set; } 
        public string DoctorName { get; set; } 
    }
}

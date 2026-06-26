using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Appointment : BaseEntity
    {
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public string AppointmentType { get; set; }
        public string ReasonForVisit { get; set; }
        public DateTime BookingDate { get; set; }
        public int PatientID { get; set; } // ==> FK
        public Patient Patient { get; set; } // ==> Navigation Property
        public int DoctorID { get; set; } // ==> FK
        public Doctor Doctor { get; set; } // ==> Navigation Property
    }
}

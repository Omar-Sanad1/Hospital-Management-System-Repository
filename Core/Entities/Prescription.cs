using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Prescription : BaseEntity
    {
        public string Notes { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public int DoctorID { get; set; } // ==> FK
        public Doctor Doctor { get; set; } // ==> Navigation Property
        public int PatientID { get; set; } // ==> FK
        public Patient Patient { get; set; } // ==> Navigation Property
    }
}

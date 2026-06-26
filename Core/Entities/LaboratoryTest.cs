using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class LaboratoryTest : BaseEntity
    {
        public string TestType { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ResultDate { get; set; }
        public string Status { get; set; }
        public int DoctorID { get; set; } // ==> FK
        public Doctor Doctor { get; set; } // ==> Navigation Property
        public int PatientID { get; set; } // ==> FK
        public Patient Patient { get; set; } // ==> Navigation Property
    }
}

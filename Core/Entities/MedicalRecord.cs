using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class MedicalRecord : BaseEntity
    {
        public string Diagnosis { get; set; }
        public string Symptoms { get; set; }
        public string TreatmentPlan { get; set; }
        public string DoctorNotes { get; set; }
        public DateTime VisitDate { get; set; }
        public string FollowUpRecommendations { get; set; }
        public int DoctorID { get; set; } // ==> FK
        public Doctor Doctor { get; set; } // ==> Navigation Property
        public int PatientID { get; set; } // ==> FK
        public Patient Patient { get; set; } // ==> Navigation Property

    }
}

using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class MedicalRecordToReturnDTO
    {
        public string Diagnosis { get; set; }
        public string Symptoms { get; set; }
        public string TreatmentPlan { get; set; }
        public string DoctorNotes { get; set; }
        public DateTime VisitDate { get; set; }
        public string FollowUpRecommendations { get; set; }
        public string DoctorName { get; set; } 
        public string PatientName { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Doctor : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public string Qualifications { get; set; }
        public DateTime HiringDate { get; set; }
        public decimal ConsultationFee { get; set; }
        public string EmploymentStatus { get; set; }
        public int DepartmentID { get; set; } // ==> FK
        public Department Department { get; set; } // ==> NavigationProperty
        public int UserID { get; set; } // ==> FK
        public User User { get; set; } // ==> NavigationProperty
        public List<MedicalRecord> MedicalRecords { get; set; } = new();
        public List<Prescription> Prescriptions { get; set; } = new();
        public List<LaboratoryTest> LaboratoryTests { get; set; } = new();
        public List<Appointment> Appointments { get; set; } = new();

    }
}

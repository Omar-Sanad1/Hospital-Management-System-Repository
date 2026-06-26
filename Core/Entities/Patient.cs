using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Patient : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string BloodType { get; set; }
        public string Address { get; set; }
        public string EmergencyContactInformation { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int UserID { get; set; } // ==> FK
        public User User { get; set; } // ==> Navigation Property
        public List<MedicalRecord> MedicalRecords { get; set; } = new();
        public List<Prescription> Prescriptions { get; set; } = new();
        public List<Bill> Bills { get; set; } = new();
        public List<LaboratoryTest> LaboratoryTests { get; set; } = new();
        public List<Appointment> Appointments { get; set; } = new();


    }
}

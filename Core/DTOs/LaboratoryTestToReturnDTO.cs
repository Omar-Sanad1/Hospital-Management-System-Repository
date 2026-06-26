using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class LaboratoryTestToReturnDTO
    {
        public string TestType { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ResultDate { get; set; }
        public string Status { get; set; }
        public string DoctorName { get; set; } 
        public string PatientName { get; set; } 
    }
}

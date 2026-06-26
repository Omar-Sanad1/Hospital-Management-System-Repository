using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class PrescriptionToReturnDTO
    {
        public int ID { get; set; }
        public string Notes { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string DoctorName { get; set; } 
        public string PatientName { get; set; } 
    }
}

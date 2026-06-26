using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Filtering
{
    public class AppointmentFiltering
    {
        public DateOnly? AppointmentDate { get; set; }
        public TimeOnly? AppointmentTime { get; set; }
        public int? PatientID { get; set; } 
        public int? DoctorID { get; set; }

        // Sorting
        public string? SortBy { get; set; }
        public bool isDescending { get; set; }
    }
}

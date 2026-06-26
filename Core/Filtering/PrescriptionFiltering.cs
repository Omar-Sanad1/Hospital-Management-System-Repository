using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Filtering
{
    public class PrescriptionFiltering
    {
        public DateTime? PrescriptionDate { get; set; }
        public int? DoctorID { get; set; } 
        public int? PatientID { get; set; }

        // Sorting
        public string? SortBy { get; set; }
        public bool isDescending { get; set; }

    }
}

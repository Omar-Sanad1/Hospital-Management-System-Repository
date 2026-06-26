using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Filtering
{
    public class DoctorFiltering
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Specialization { get; set; }
        public int? DepartmentID { get; set; }

        // Sorting
        public string? SortBy { get; set; }
        public bool isDescending { get; set; }

    }
}

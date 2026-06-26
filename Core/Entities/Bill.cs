using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Bill : BaseEntity
    {
        public DateTime BillDate { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string PaymentStatus { get; set; }
        public List<Payment> Payments { get; set; } = new();
        public int PatientID { get; set; } // ==> FK
        public Patient Patient { get; set; } // ==> Navigation Property
    }
}

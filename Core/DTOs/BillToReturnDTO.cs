using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class BillToReturnDTO
    {
        public int ID { get; set; }
        public DateTime BillDate { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string PaymentStatus { get; set; }
        public string PatientName { get; set; } 

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Payment : BaseEntity
    {
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionReferenceNumber { get; set; }
        public int BillID { get; set; } // ==> FK
        public Bill Bill { get; set; } // ==> Navigation Property
    }
}

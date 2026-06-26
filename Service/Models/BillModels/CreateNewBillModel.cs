using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.BillModels
{
    public class CreateNewBillModel
    {
        public decimal TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string PaymentStatus { get; set; }
        public int PatientID { get; set; } 
    }
}

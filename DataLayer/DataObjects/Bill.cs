using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataObjects
{
    public class Bill
    {
        public int BillId { get; set; }
        public int UserId { get; set; }
        public DateTime BillDate { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}

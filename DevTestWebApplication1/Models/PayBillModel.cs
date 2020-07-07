using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTestWebApplication1.Models
{
    public class PayBillModel
    {
        public int BillId { get; set; }
        public string CreditCard { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationMonth { get; set; }
    }
}

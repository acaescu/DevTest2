using DataLayer.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IBilling
    {
        List<Bill> GetUserBills(int userId, int start, int length, out int totalRecords);
        void PayBill(int billId, string creditCard, string cardHolder, DateTime expirationDate);
    }
}

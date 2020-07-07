using BusinessLayer.Interfaces;
using DataLayer.DataObjects;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public class Billing : IBilling
    {
        IDomainContext _db;
        public Billing(IDomainContext db)
        {
            _db = db;
        }
        public List<Bill> GetUserBills(int userId, int start, int length, out int totalRecords)
        {
            var query = _db.Bills.Where(a => a.UserId == userId);
            totalRecords = query.Count();
            return query.OrderByDescending(a => a.BillDate).Skip(start).Take(length).ToList();
        }
        // TO DO: verify that bill is not already paid, record transactions,verify card, other checks
        public void PayBill(int billId, string creditCard, string cardHolder, DateTime expirationDate)
        {
            var bill = _db.Bills.FirstOrDefault(a => a.BillId == billId);
            if (bill != null)
            {
                bill.Status = "Paid";
                bill.PaidDate = DateTime.Now;
            }
        }
    }
}

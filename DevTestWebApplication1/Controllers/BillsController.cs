using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using DevTestWebApplication1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevTestWebApplication1.Controllers
{
    
    [ApiController]
    public class BillsController : ControllerBase
    {
        ICustomMembership _membership;
        IBilling _billing;
        public BillsController(ICustomMembership membership, IBilling billing)
        {
            _membership = membership;
            _billing = billing;
        }

        [HttpPost]
        [Route("api/Bills/UserBills")]
        public object UserBills([FromForm]DataTablesRequest model)
        {
            int totalRecords;
            var data = _billing.GetUserBills(_membership.CurrentUser.UserId, model.start, model.length, out totalRecords);
            return new { model.draw, data, recordsTotal = totalRecords, recordsFiltered = totalRecords };
        }

        [HttpPost]
        [Route("api/Bills/PayBill")]
        public void PayBill([FromForm]PayBillModel model)
        {
            _billing.PayBill(model.BillId, model.CreditCard, model.CardHolder, model.ExpirationMonth);
        }
    }
}
using DataLayer.DataObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICustomMembership
    {
        User CurrentUser { get; }
        Task<bool> Login(string userName, string password);
        bool VerifyLogin(string userName, string password, out User user);
        Task LogOff();
        Task<bool> ValidateLogin(ClaimsPrincipal principal, HttpRequest request);
    }
}

using BusinessLayer.Interfaces;
using DataLayer.DataObjects;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CustomMembership : ICustomMembership
    {
        IHttpContextAccessor _context;
        IDomainContext _db;
        public CustomMembership(IHttpContextAccessor context, IDomainContext db)
        {
            _context = context;
            _db = db;
        }

        public User CurrentUser { get {
                var sessionId = _context.HttpContext.User.FindFirst("sessionId");
                if (sessionId != null)
                {
                    return _db.Users.FirstOrDefault(a => a.SessionId == new Guid(sessionId.Value));
                }
                return null;
            }  
        }

        public async Task<bool> Login(string userName, string password)
        {
            User user;
            if (VerifyLogin(userName, password, out user))
            {
                user.SessionId = Guid.NewGuid();
                //some of those values should come from the config
                var identity = new ClaimsIdentity("Cookies");
                identity.AddClaim(new Claim("sessionId", user.SessionId.ToString()));
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                    IsPersistent = true
                };
                await _context.HttpContext.SignInAsync(new ClaimsPrincipal(identity), properties);
                return true;
            }
            return false;
        }

        public async Task LogOff()
        {
            var sessionId = _context.HttpContext.User.FindFirst("sessionId");
            if (sessionId != null)
            {
                var user = _db.Users.FirstOrDefault(a => a.SessionId == new Guid(sessionId.Value));
                if (user != null)
                {
                    user.SessionId = null;
                }
            }
            await _context.HttpContext.SignOutAsync();
            
        }

        public async Task<bool> ValidateLogin(ClaimsPrincipal principal, HttpRequest request)
        {
            var sessionId = principal.FindFirst("sessionId").Value;
            if (sessionId == null)
            {
                return false;
            }
            var user = _db.Users.FirstOrDefault(a => a.SessionId == new Guid(sessionId));
            if (user == null)
            {
                return false;
            }
            //so far so good, the user is authenticated, now we will check if he is authorized to access the resource
            string path = request.Path;
            if (path == "/" || path.ToLower() == "/home/index")
            {
                return true;
            }
            return user.Roles.Any(a => a.Permissions.Any(b => b.Url.ToLower() == path.ToLower()));
        }

        public bool VerifyLogin(string userName, string password, out User user)
        {
            user = _db.Users.FirstOrDefault(a => a.UserName == userName);
            if (user != null)
            {
                var passwordSalt = user.PasswordSalt ?? "";
                var hash = new EncryptionHelper().GeneratePasswordHash(password, ref passwordSalt);
                return user.PasswordHash == new EncryptionHelper().GeneratePasswordHash(password, ref passwordSalt);
            }
            return false;
        }
    }
}

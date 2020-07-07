using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataObjects
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Guid? SessionId { get; set; }
        public List<Role> Roles { get; set; }
    }
}

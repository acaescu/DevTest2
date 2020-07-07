using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataObjects
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string DefaultRedirect { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}

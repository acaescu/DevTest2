using DataLayer.DataObjects;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DomainContext : IDomainContext
    {
        public DomainContext()
        {
            Permissions = new List<Permission>
            { 
                new Permission
                {
                    PermissionId = 1,
                    Url = "/home/admindashboard"
                },
                new Permission
                {
                    PermissionId = 2,
                    Url = "/home/clientdashboard"
                },
                new Permission
                {
                    PermissionId = 3,
                    Url = "/api/bills/userbills"
                },
                new Permission
                {
                    PermissionId = 4,
                    Url = "/api/bills/paybill"
                }
            };
            Roles = new List<Role>
            {
                new Role
                {
                    RoleId = 1,
                    RoleName = "admin",
                    DefaultRedirect = "/home/admindashboard",
                    Permissions = Permissions.Where(a => a.PermissionId == 1).ToList()
                },
                new Role
                {
                    RoleId = 2,
                    RoleName = "client",
                    DefaultRedirect = "/home/clientdashboard",
                    Permissions = Permissions.Where(a => a.PermissionId == 2 || a.PermissionId == 3 || a.PermissionId == 4).ToList()
                }
            };
            Users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    UserName = "admin",
                    PasswordHash = "�����g\u001b]e�\u001cހ\u0011p�Rg\\^3\u0004#\0R��Ul�\u0005{\u001c���\u0014\f������:�\0��ˑ��\u001c\u0004>L\r����P�",
                    PasswordSalt = "�\tp�����ى���\u0002���\u0014wY�#�[",
                    Roles = this.Roles.Where(a => a.RoleId == 1).ToList()
                },
                new User
                {
                    UserId = 2,
                    UserName = "client",
                    PasswordHash = "�����g\u001b]e�\u001cހ\u0011p�Rg\\^3\u0004#\0R��Ul�\u0005{\u001c���\u0014\f������:�\0��ˑ��\u001c\u0004>L\r����P�",
                    PasswordSalt = "�\tp�����ى���\u0002���\u0014wY�#�[",
                    Roles = this.Roles.Where(a => a.RoleId == 2).ToList()
                }
            };

            Bills = new List<Bill>
            {
                new Bill
                {
                    BillId = 1,
                    UserId = 2,
                    BillDate = DateTime.Parse("06/01/2020"),
                    Status = "Paid",
                    Amount = 79.23M,
                    PaidDate = DateTime.Parse("06/06/2020"),
                },
                new Bill
                {
                    BillId = 2,
                    UserId = 2,
                    BillDate = DateTime.Parse("07/01/2020"),
                    Status = "NotPaid",
                    Amount = 73.11M
                }
            };
        }

        public List<Bill> Bills { get; set; }
        public List<Permission> Permissions { get; set; }
        public List<User> Users { get; set; }
        public List<Role> Roles { get; set; }
    }
}

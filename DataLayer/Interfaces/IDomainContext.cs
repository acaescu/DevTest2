using DataLayer.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IDomainContext
    {
        List<Bill> Bills { get; set; }
        List<Permission> Permissions { get; set; }
        List<User> Users { get; set; }

    }
}

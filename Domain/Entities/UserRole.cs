using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class UserRole(string role, string description, IEnumerable<string> rolesNeeds)
{
    public string Role = role;
    public string Description = description;
    public IEnumerable<string> RolesNeeds = rolesNeeds;
}

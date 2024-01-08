namespace Domain.Entities;
public class UserRole(string role, string description, IEnumerable<string> rolesNeeds)
{
    public string Role = role;
    public string Description = description;
    public IEnumerable<string> RolesNeeds = rolesNeeds;
}

namespace Application.Common.Authorization;

public class Role(string role, string description, IEnumerable<string> rolesNeeds)
{
    public string Value = role;
    public string Description = description;
    public IEnumerable<string> RolesNeeds = rolesNeeds;
}
using Domain.Common;

namespace Domain.Entities;
public class Profile(string Name, bool isAdmin, string[]? Roles) : EntityBase
{
    public string Name { get; private set; } = Name;
    public string[] Roles { get; private set; } = Roles ?? [];
    public bool IsAdmin { get; private set; } = isAdmin;
    public IReadOnlyCollection<User> Users { get; private set; } = [];
}

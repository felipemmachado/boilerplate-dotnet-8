using Domain.Common;

namespace Domain.Entities;
public class User : EntityBase
{
    public string Name { get; protected set; }
    public string Email { get; protected set; }
    public string Password { get; protected set; }
    public bool ForceChangePassword { get; protected set; }
    public DateTime? DisabledAt { get; protected set; }
    public DateTime? FirstAccess { get; protected set; }
    public DateTime? LastAccess { get; protected set; }
    public IEnumerable<string> Roles { get; protected set; }

    public User(string name, string email, string password, IEnumerable<string> roles)
    {
        Name = name;
        Email = email.ToLower();
        Password = password;
        Roles = roles;
    }

    public void Update(string name)
    {
        Name = name;
    }

    public void UpdateRoles(IEnumerable<string> roles)
    {
        Roles = roles;
    }

    public void UpdateEmail(string email)
    {
        Email = email;
    }

    public void SetForceChangePassword(bool force) { ForceChangePassword = force; }

    public bool IsDisabled()
    {
        return DisabledAt != null;
    }

    public void Disabled()
    {
        DisabledAt = DateTime.UtcNow;
    }

    public void Enabled()
    {
        DisabledAt = null;
    }

    public void UpdatePassword(string newPassword)
    {
        Password = newPassword;
    }

    public void UpdateLastAccess(DateTime when)
    {
        LastAccess = when;
    }

    public void UpdateFirstAccess(DateTime when)
    {
        FirstAccess = when;
    }

}

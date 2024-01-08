using Domain.Common;

namespace Domain.Entities;
public class User(Guid profileId, string name, string email, string passwordHash) : EntityBase
{
    public Guid ProfileId { get; protected set; } = profileId;
    public string Name { get; protected set; } = name;
    public string Email { get; protected set; } = email.ToLower();
    public string PasswordHash { get; protected set; } = passwordHash;
    public bool RequestChangePassword { get; protected set; } = true;
    public DateTime? DisabledAt { get; protected set; }
    public DateTime? FirstAccess { get; protected set; }
    public DateTime? LastAccess { get; protected set; }
    public Profile? Profile { get; protected set; }

    public void Update(string name)
    {
        Name = name;
    }

    public void UpdateEmail(string email)
    {
        Email = email;
    }

    public bool IsDisabled()
    {
        return DisabledAt != null;
    }

    public void ChangePassword()
    {
        RequestChangePassword = false;
    }

    public void Disabled()
    {
        DisabledAt = DateTime.UtcNow;
    }

    public void Enabled()
    {
        DisabledAt = null;
    }

    public void UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    public void ChangeProfile(Guid profileId)
    {
        ProfileId = profileId;
    }

    public void UpdateLastAccess()
    {
        LastAccess = DateTime.UtcNow;
    }

    public void UpdateFirstAccess()
    {
        FirstAccess = DateTime.UtcNow;
    }

}

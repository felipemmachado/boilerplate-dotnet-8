namespace Application.Common.Authorization;
public static class Role
{
    public const string Users = "user";

    public static IEnumerable<string> GetAdminRoles()
    {
        var adminRoles = new List<string>
        {
            Users
        };


        return adminRoles;
    }
}

namespace Application.Common.Authorization;

public class Roles
{
    public const string Settings = nameof(Settings);
    public static readonly Role AccessSettingsRole = new(Settings, "Permite acessar as configurações do sistema", []);

    public const string Users = nameof(Users);
    public static readonly Role UserRole = new(Users, "Permite acessar o cadastro/edição de usuários", [Settings]);

    public static readonly IReadOnlyCollection<Role> AllRoles = [
        AccessSettingsRole,
        UserRole
    ];

    public static bool Exists(string role)
    {
        var list = AllRoles.Select(x => x.Value).ToList();
        return list.Contains(role);
    }
}



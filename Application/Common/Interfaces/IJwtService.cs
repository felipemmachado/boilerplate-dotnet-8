namespace Application.Common.Interfaces;
public interface IJwtService
{
    string ApplicationAccessToken(
        string userId,
        IEnumerable<string> roles);

    string PasswordToken(
        string userId,
        string name);

    Task<bool> ValidPasswordToken(string token);
}



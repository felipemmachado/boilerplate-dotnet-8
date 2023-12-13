namespace Application.Common.Interfaces;

public interface IEmailService
{
    Task<bool> ForgotPassword(string email, string name, string link);
    Task<bool> FirstAccess(string email, string name, string password, string link);
}


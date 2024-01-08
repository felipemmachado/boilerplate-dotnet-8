namespace Application.Common.Interfaces;
public interface IPasswordService
{
    string Hash(string password);
    string GenerateRandomPassword();
    bool Verify(string chave, string password);
}

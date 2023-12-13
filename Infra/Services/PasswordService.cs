using Application.Common.Exceptions;
using Application.Common.Interfaces;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Infra.Services;
public class PasswordService : IPasswordService
{
    private static readonly int Iterations = 10000; //
    private static readonly int SaltSize = 16; // 128 bit
    private static readonly int KeySize = 32; // 256 bit

    public string Generate(string password, bool validade)
    {
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMinimum6Chars = new Regex(@".{6,}");

        if (validade)
        {
            if (!hasNumber.IsMatch(password))
                throw new ValidationException("Password", "A senha tem que ter pelo menos um número.");

            if (!hasUpperChar.IsMatch(password))
                throw new ValidationException("Password", "A senha tem que ter pelo menos uma letra maiúscula.");

            if (!hasMinimum6Chars.IsMatch(password))
                throw new ValidationException("Password", "A senha tem que ter pelo menos 6 caracteres.");
        }



        using var algorithm = new Rfc2898DeriveBytes(
           password,
           SaltSize,
           Iterations,
           HashAlgorithmName.SHA512);
        var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{salt}.{key}";
    }

    public string GetAlphanumericCode(int length)
    {
        Random random = new();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public bool Check(string chave, string password)
    {
        var parts = chave.Split('.', 2);

        if (parts.Length != 2)
            return false;

        var salt = Convert.FromBase64String(parts[0]);
        var key = Convert.FromBase64String(parts[1]);

        using var algorithm = new Rfc2898DeriveBytes(
          password,
          salt,
          Iterations,
          HashAlgorithmName.SHA512);
        var keyToCheck = algorithm.GetBytes(KeySize);

        var verified = keyToCheck.SequenceEqual(key);

        return verified;
    }
}


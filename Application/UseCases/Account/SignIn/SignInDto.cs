namespace Application.UseCases.Account.SignIn;
public record struct SignInDto
{
    public string AccessToken { get; set; }
    public bool TemporaryPassword { get; set; }
}


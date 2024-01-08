namespace Application.UseCases.Users.CreateUser;
public record struct CreateUserDto
{
    public Guid Id { get; set; }
    public string TemporaryPassword { get; set; }
}


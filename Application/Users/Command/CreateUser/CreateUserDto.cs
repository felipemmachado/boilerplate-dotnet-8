namespace Application.Users.Command.CreateUser;
public record struct CreateUserDto
{
    public Guid Id { get; set; }
    public string TemporaryPassword { get; set; }
}


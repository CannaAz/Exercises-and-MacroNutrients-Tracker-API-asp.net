namespace api.Dtos.Account;

public record class RegisterDto
{
    public string? Username { get; set; }

    public string? Email { get; set; }
    public string? Password { get; set; }
}

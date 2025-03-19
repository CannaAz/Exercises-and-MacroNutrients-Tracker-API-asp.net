namespace api.Dtos.Account;

public record class loginDto
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}

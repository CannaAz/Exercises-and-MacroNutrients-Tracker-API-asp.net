namespace api;

public record class NewUserDto
{
    public string? UserName { get; set; }
    public string? Email { get; set; }

    public string Token { get; set; }
}

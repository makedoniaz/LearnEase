namespace LearnEase.Core.Dtos;

public class LoginDto
{
    public required string Login { get; set; }
    public required string Password { get; set; }
    public string? ReturnUrl { get; set; }
}

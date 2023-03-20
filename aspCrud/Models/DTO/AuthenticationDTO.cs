namespace aspCrud.Models.DTO;

public class LoginDTO
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponseDTO
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpireDateTime { get; set; }
}

public class TokenOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
}
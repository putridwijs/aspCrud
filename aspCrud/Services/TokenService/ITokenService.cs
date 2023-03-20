namespace aspCrud.Services.TokenService;

public interface ITokenService
{
    LoginResponseDTO GenerateToken(Guid id, string email);
}
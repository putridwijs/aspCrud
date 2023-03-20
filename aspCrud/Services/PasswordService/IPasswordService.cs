namespace aspCrud.Services.PasswordService;

public interface IPasswordService
{
    Task<bool> CheckPassword(Guid id, string password);
    string HashPassword(string password);
}
using System.Security.Cryptography;

namespace aspCrud.Services.PasswordService;

public class PasswordService : IPasswordService
{
    private readonly IUserRepository _userRepository;
    private readonly int _defaultIteration = 100000;
    private readonly int _saltSize = 16;
    private readonly int _keySize = 32;

    public PasswordService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> CheckPassword(Guid id, string password)
    {
        var userPassword = await _userRepository.GetUser(id);
        if (userPassword == null || userPassword.Password == string.Empty)
            return false;

        var parts = userPassword.Password.Split('.');
        if (parts.Length != 2)
            throw new FormatException("Unexpected hash format");

        var salt = Convert.FromBase64String(parts[0]);
        var key = Convert.FromBase64String(parts[1]);

        using var algoritm = new Rfc2898DeriveBytes(password, salt, _defaultIteration);
        var keyToCheck = algoritm.GetBytes(_keySize);
        return keyToCheck.SequenceEqual(key);
    }
    
    public string HashPassword(string password)
    {
        var algoritm = new Rfc2898DeriveBytes(password, saltSize: _saltSize, iterations: _defaultIteration);
        var key = Convert.ToBase64String(algoritm.GetBytes(_keySize));
        var salt = Convert.ToBase64String(algoritm.Salt);

        return $"{salt}.{key}";
    }
}
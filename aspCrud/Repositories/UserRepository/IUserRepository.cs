using aspCrud.Models.DAO;

namespace aspCrud.Repositories.UserRepository;

public interface IUserRepository
{
    Task<List<UserDAO>> GetUsers();
    Task<UserDAO?> GetUser(Guid id);
    Task<UserDAO?> AddUser(UserDAO user);
    Task<UserDAO?> UpdateUser(Guid id, UserDAO request);
    Task<bool> DeleteUser(Guid id);
}
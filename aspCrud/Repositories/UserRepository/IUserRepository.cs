using aspCrud.Models.DAO;

namespace aspCrud.Repositories.UserRepository;

public interface IUserRepository
{
    Task<GetAllResponseDTO<UserDAO>> GetUsers(int pageSize, int skipAmount);
    Task<UserDAO?> GetUser(Guid id);
    Task<UserDAO?> AddUser(UserDAO user);
    Task<UserDAO?> UpdateUser(Guid id, UserDAO request);
    Task<bool> DeleteUser(Guid id);
    Task<UserDAO?> GetUserByEmail(string email);
}
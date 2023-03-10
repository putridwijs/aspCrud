using aspCrud.Models.DTO;

namespace aspCrud.Services.UserService;

public interface IUserService
{
    Task<List<UserResponseDTO>> GetUsers();
    Task<UserResponseDTO?> GetUser(Guid id);
    Task<UserResponseDTO?> AddUser(UserDTO request);
    Task<UserResponseDTO?> UpdateUser(Guid id, UserDTO request);
    Task<bool> DeleteUser(Guid id);
}
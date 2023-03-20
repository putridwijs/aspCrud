namespace aspCrud.Services.UserService;

public interface IUserService
{
    Task<ResponsePaginationDTO<UserResponseDTO>> GetUsers(int? page, int? pageSize);
    Task<UserResponseDTO?> GetUser(Guid id);
    Task<UserResponseDTO?> AddUser(UserDTO request);
    Task<UserResponseDTO?> UpdateUser(Guid id, UserDTO request);
    Task<bool> DeleteUser(Guid id);
    Task<UserResponseDTO?> GetUserByEmail(string email);
    Task<bool> CheckExistingUserByEmail(string email);
}
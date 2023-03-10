using aspCrud.Models.DAO;
using aspCrud.Models.DTO;
using aspCrud.Repositories.UserRepository;
using AutoMapper;

namespace aspCrud.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }
    
    // Get All Users
    public async Task<List<UserResponseDTO>> GetUsers()
    {
        var users = await _userRepository.GetUsers();
        var response = _mapper.Map<List<UserResponseDTO>>(users);
        return response;
    }
    
    // Get User By Id
    public async Task<UserResponseDTO?> GetUser(Guid id)
    {
        var user = await _userRepository.GetUser(id);
        if (user == null)
            return null;
        var response = _mapper.Map<UserResponseDTO>(user);
        return response;
    }
    
    // Add User
    public async Task<UserResponseDTO?> AddUser(UserDTO request)
    {
        var role = await _roleRepository.CheckRoleId(request.RoleId);
        if (role == false)
            return null;
        var userDAO = _mapper.Map<UserDAO>(request);
        var result = await _userRepository.AddUser(userDAO);
        if (result == null)
            return null;
        var response = _mapper.Map<UserResponseDTO>(result);
        return response;
    }
    
    // Edit User
    public async Task<UserResponseDTO?> UpdateUser(Guid id, UserDTO request)
    {
        var userDAO = _mapper.Map<UserDAO>(request);
        var result = await _userRepository.UpdateUser(id, userDAO);
        if (result == null)
            return null;
        var response = _mapper.Map<UserResponseDTO>(result);
        return response;
    }
    
    // Delete User
    public async Task<bool> DeleteUser(Guid id)
    {
        return await _userRepository.DeleteUser(id);
    }
}
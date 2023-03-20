using aspCrud.Models.DAO;
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
    public async Task<ResponsePaginationDTO<UserResponseDTO>> GetUsers(int? page, int? pageSize)
    {
        var skipAmount = pageSize * (page - 1);
        var users = await _userRepository.GetUsers(pageSize ?? 10, skipAmount ?? 1);
        var response = _mapper.Map<List<UserResponseDTO>>(users.Result);
        
        var mod = users.Count % pageSize;
        var totalPage = (users.Count / pageSize) + (mod == 0 ? 0 : 1);

        return new ResponsePaginationDTO<UserResponseDTO>()
        {
            PageSize = pageSize,
            PageNumber = page,
            Results = response,
            TotalNumberOfPages = totalPage,
            TotalNumberOfRecords = users.Count
        };
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
    
    // Get User By Email
    public async Task<UserResponseDTO?> GetUserByEmail(string email)
    {
        var user = await _userRepository.GetUserByEmail(email);
        if (user == null)
            return null;
        var response = _mapper.Map<UserResponseDTO>(user);
        return response;
    }
    
    // Check Existing User by Email
    public async Task<bool> CheckExistingUserByEmail(string email)
    {
        var user = await _userRepository.GetUserByEmail(email);
        if (user == null)
            return false;
        return true;
    }
}
using aspCrud.Models.DAO;
using Microsoft.EntityFrameworkCore;

namespace aspCrud.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly BEDBContext _bedbContext;

    public UserRepository(BEDBContext bedbContext)
    {
        _bedbContext = bedbContext;
    }
    
    // Get All Users
    public async Task<GetAllResponseDTO<UserDAO>> GetUsers(int pageSize, int skipAmount)
    {
        var result = await _bedbContext.Users.Skip(skipAmount).Take(pageSize).ToListAsync();
        var totalData = await _bedbContext.Users.CountAsync();
        return new GetAllResponseDTO<UserDAO>()
        {
            Result = result,
            Count = totalData
        };
    }
    
    // Get User By Id
    public async Task<UserDAO?> GetUser(Guid id)
    {
        return await _bedbContext.Users.FindAsync(id);
    }
    
    // Create User
    public async Task<UserDAO?> AddUser(UserDAO user)
    {
        await _bedbContext.Users.AddAsync(user);
        var result = await _bedbContext.SaveChangesAsync() == 1;
        return result ? user : null;
    }
    
    // Edit User
    public async Task<UserDAO?> UpdateUser(Guid id, UserDAO request)
    {
        var user = await _bedbContext.Users.FindAsync(id);
        if (user == null)
            return null;

        request.Id = user.Id;
        
        _bedbContext.Users.Update(request);
        var result = await _bedbContext.SaveChangesAsync() == 1;
        return result ? user : null;
    }
    
    // Delete User
    public async Task<bool> DeleteUser(Guid id)
    {
        var user = await _bedbContext.Users.FindAsync(id);
        if (user == null)
            return false;
        _bedbContext.Users.Remove(user);
        var result = await _bedbContext.SaveChangesAsync() == 1;
        return result;
    }
    
    // Get User By Email
    public async Task<UserDAO?> GetUserByEmail(string email)
    {
        var user = await _bedbContext.Users.Where(x=> x.Email == email).FirstOrDefaultAsync();
        if (user == null)
            return null;
        return user;
    }
}
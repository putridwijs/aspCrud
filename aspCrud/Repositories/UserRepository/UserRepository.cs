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
    public async Task<List<UserDAO>> GetUsers()
    {
        return await _bedbContext.Users.ToListAsync();
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
}
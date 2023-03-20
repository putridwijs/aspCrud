using aspCrud.Models.DAO;
using Microsoft.EntityFrameworkCore;

namespace aspCrud.Repositories.RoleRepository
{
    public class RoleRepository: IRoleRepository
    {
        private readonly BEDBContext _bedbContext;

        public RoleRepository(BEDBContext bedbContext)
        {
            _bedbContext = bedbContext;
        }

        // Get All Roles
        public async Task<List<RoleDAO>> GetRoles()
        {
            return await _bedbContext.Roles.ToListAsync();
        }
        
        // Get Role By Id
        public async Task<RoleDAO?> GetRole(Guid id)
        {
            var result = await _bedbContext.Roles.FindAsync(id);
            if (result == null)
                return null;
            return result;
        }

        // Create Role
        public async Task<RoleDAO?> AddRole(RoleDAO role)
        {
            // role.CreatedAt = DateTime.Now;
            await _bedbContext.Roles.AddAsync(role);
            var result = await _bedbContext.SaveChangesAsync() == 1;
            return result ? role : null;
        }
        
        // Update Role
        public async Task<RoleDAO?> EditRole(Guid id, RoleDAO request)
        {
            var role = await _bedbContext.Roles.FindAsync(id);
            if (role == null)
                return null;

            role.Name = request.Name;
            role.IsActive = request.IsActive;

            _bedbContext.Roles.Update(role);
            var result = await _bedbContext.SaveChangesAsync() == 1;
            return result ? role: null;
        }
        
        // Delete Role
        public async Task<bool?> DeleteRole(Guid id)
        {
            var role = await _bedbContext.Roles.FindAsync(id);
            if (role == null)
                return null;
            _bedbContext.Roles.Remove(role);
            var result = await _bedbContext.SaveChangesAsync() == 1;
            return result;
        }

        public async Task<bool> CheckRoleId(Guid roleId)
        {
            var role = await _bedbContext.Roles.FindAsync(roleId);
            if (role == null)
                return false;
            return true;
        }
    }
}


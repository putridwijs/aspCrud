using aspCrud.Models.DAO;

namespace aspCrud.Repositories.RoleRepository
{
    public interface IRoleRepository
    {
        Task<List<RoleDAO>> GetRoles();
        Task<RoleDAO?> GetRole(Guid id);
        Task<RoleDAO?> AddRole(RoleDAO role);
        Task<RoleDAO?> EditRole(Guid id, RoleDAO request);
        Task<bool?> DeleteRole(Guid id);
        Task<bool> CheckRoleId(Guid roleId);
    }
}
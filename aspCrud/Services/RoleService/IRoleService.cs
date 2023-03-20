namespace aspCrud.Services.RoleService
{
    public interface IRoleService
    {
        Task<List<RoleResponseDTO>> GetRoles();
        Task<RoleResponseDTO?> GetRole(Guid id);
        Task<RoleResponseDTO> AddRole(RoleDTO request);
        Task<RoleResponseDTO?> EditRole(Guid id, RoleDTO request);
        Task<bool?> DeleteRole(Guid id);
    }
}

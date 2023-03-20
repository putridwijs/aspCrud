using aspCrud.Models.DAO;
using AutoMapper;

namespace aspCrud.Services.RoleService
{
    public class RoleService: IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        // Get All Roles
        public async Task<List<RoleResponseDTO>> GetRoles()
        {
            var data = await _roleRepository.GetRoles();
            var response = _mapper.Map<List<RoleResponseDTO>>(data);
            return response;
        }
        
        // Get Role By Id
        public async Task<RoleResponseDTO?> GetRole(Guid id)
        {
            var result = await _roleRepository.GetRole(id);
            if (result == null)
                return null;
            var response = _mapper.Map<RoleResponseDTO>(result);
            return response ;
        }

        // Create Role
        public async Task<RoleResponseDTO> AddRole(RoleDTO request)
        {
            var role = new RoleDAO
            {
                Name = request.Name,
                IsActive = request.IsActive
            };

            var result = await _roleRepository.AddRole(role);
            var response = _mapper.Map<RoleResponseDTO>(result);
            return response;
        }
        
        // Update Role
        public async Task<RoleResponseDTO?> EditRole(Guid id, RoleDTO request)
        {
            var role = new RoleDAO
            {
                Name = request.Name,
                IsActive = request.IsActive,
            };
            var result = await _roleRepository.EditRole(id, role);
            var response = _mapper.Map<RoleResponseDTO>(result);
            return response;
        }
        
        // Delete Role
        public async Task<bool?> DeleteRole(Guid id)
        {
            return await _roleRepository.DeleteRole(id);
        }
    }
}

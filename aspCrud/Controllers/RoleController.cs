using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspCrud.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<RoleResponseDTO>>> GetRoles()
        {
            return Ok(await _roleService.GetRoles());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleResponseDTO>> GetRole(Guid id)
        {
            var response = await _roleService.GetRole(id);
            if (response == null)
                return NotFound("role not found.");
            return Ok(response);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<RoleResponseDTO>> AddRole(RoleDTO role)
        {
            // var result = await _roleService.AddRole(role);
            return Ok(await _roleService.AddRole(role));
        }

        [HttpPut("{id}"), Authorize]
        public async Task<ActionResult<RoleResponseDTO>> EditRole(Guid id, RoleDTO request)
        {
            var response = await _roleService.EditRole(id, request);
            if (response == null)
                return NotFound("role not found.");
            return Ok(response);
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<bool>> DeleteRole(Guid id)
        {
            var response = await _roleService.DeleteRole(id);
            if (response == null)
                return NotFound("role not found.");
            return Ok(response);
        }
    }
}
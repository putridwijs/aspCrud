using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspCrud.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    // GET All Users
    [HttpGet]
    public async Task<ActionResult<ResponsePaginationDTO<UserResponseDTO>>> GetUsers([FromQuery(Name = "page")] int? page = 1, [FromQuery(Name = "pageSize")] int? pageSize = 10)
    {
        return Ok(await _userService.GetUsers(page, pageSize));
    }
    
    // GET User By Id
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDTO>> GetUser(Guid id)
    {
        var response = await _userService.GetUser(id);
        if (response == null)
            return NotFound("user not found");
        return Ok(response);
    }
    
    // POST Add User
    [HttpPost, Authorize]
    public async Task<ActionResult<UserResponseDTO>> AddUser(UserDTO request)
    {
        var response = await _userService.AddUser(request);
        if (response == null)
            return BadRequest();
        return Ok(response);
    }
    
    // PUT Edit User
    [HttpPut, Authorize]
    public async Task<ActionResult<UserResponseDTO>> UpdateUser(Guid id, UserDTO request)
    {
        var response = await _userService.UpdateUser(id, request);
        if (response == null)
            return BadRequest();
        return Ok(response);
    }
    
    // DELETE User
    [HttpDelete, Authorize]
    public async Task<ActionResult<bool>> DeleteUser(Guid id)
    {
        var response = await _userService.DeleteUser(id);
        return response ? Ok(response): BadRequest();
    }
}
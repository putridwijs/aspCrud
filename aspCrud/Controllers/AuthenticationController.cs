using aspCrud.Services.PasswordService;
using aspCrud.Services.TokenService;
using Microsoft.AspNetCore.Mvc;

namespace aspCrud.Controllers;

[Route("api/auth")]
[ApiController]

public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;

    public AuthenticationController(IUserService userService, IPasswordService passwordService, ITokenService tokenService)
    {
        _userService = userService;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }
    
    // GET
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDTO>> Login(LoginDTO request)
    {
        var user = await _userService.GetUserByEmail(request.Email);
        if (user == null)
            return NotFound("email didn't exist");

        var checkPassword = await _passwordService.CheckPassword(user.Id, request.Password);
        if (checkPassword == false)
            return BadRequest("Password incorrect.");

        var response = _tokenService.GenerateToken(user.Id, user.Email);

        return response;
    }
    
    // POST
    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDTO>> Register(UserDTO request)
    {
        if (request == null) return BadRequest();

        var userExisting = await _userService.CheckExistingUserByEmail(request.Email);
        if (userExisting)
            return BadRequest("user existing");

        var encryptedPassword = _passwordService.HashPassword(request.Password);
        request.Password = encryptedPassword;

        var response = await _userService.AddUser(request);
        if (response == null)
            return BadRequest("role not exist");
        return Ok(response);
    }
}
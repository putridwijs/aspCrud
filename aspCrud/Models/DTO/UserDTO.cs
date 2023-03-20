using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace aspCrud.Models.DTO;

public class UserDTO
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public Guid RoleId { get; set; }
}

public class UserResponseDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public Guid RoleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    [JsonIgnore]
    public string Password { get; set; } = string.Empty;
}

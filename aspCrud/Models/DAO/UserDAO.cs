namespace aspCrud.Models.DAO;

public class UserDAO : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public Guid RoleId { get; set; }
}
namespace aspCrud.Models.DTO;

public class RoleDTO
{
    public string? Name { get; set; }
    public bool IsActive { get; set; }
}

public class RoleResponseDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
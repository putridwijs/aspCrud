namespace aspCrud.Models.DAO
{
    public class RoleDAO: BaseEntity
    {
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}
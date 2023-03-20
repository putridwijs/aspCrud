using System.ComponentModel.DataAnnotations.Schema;
using aspCrud.Models.DAO;
using Microsoft.EntityFrameworkCore;

namespace aspCrud.Models
{
    public class BEDBContext: DbContext
    {
        public BEDBContext(DbContextOptions<BEDBContext> options) : base(options)
        {
            
        }

        public DbSet<RoleDAO> Roles { get; set; }
        public DbSet<UserDAO> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes()
                         .Where(x =>
                             x.ClrType.GetProperties().Any(y =>
                                 y.CustomAttributes.Any(z =>
                                     z.AttributeType == typeof(DatabaseGeneratedAttribute)))))
            {
                foreach (var property in entity.ClrType.GetProperties()
                             .Where(x =>
                                 x.PropertyType == typeof(DateTime) && x.CustomAttributes.Any(y=>
                                     y.AttributeType == typeof(DatabaseGeneratedAttribute))))
                {
                    modelBuilder
                        .Entity(entity.ClrType)
                        .Property(property.Name)
                        .HasDefaultValueSql("GETUTCDATE()");
                }
            }
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity &&
                            (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }

                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }
    }
}
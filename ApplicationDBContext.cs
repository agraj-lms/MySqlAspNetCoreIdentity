using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MySqlIdentity
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
       : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "server=localhost;user=root;password=toor;database=cinemass";

                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Your model configurations...

            // Example: Set charset and collation for MySQL
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetCharSet("utf8mb4");
                entityType.SetCollation("utf8mb4_general_ci");
            }
        }
    }
}

using fin_accounting_api.Domain.Resources;
using fin_accounting_api.Domain.Resources.Configs;
using fin_accounting_api.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace fin_accounting_api.Persistance
{
    public class Fin_accounting_apiContext : IdentityDbContext<ApiUser>
    {
        public Fin_accounting_apiContext()
        {
            
        }

        public Fin_accounting_apiContext(DbContextOptions<Fin_accounting_apiContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<ApiUser> Users { get; set; }
        public DbSet<UserResource> UserResources { get; set; }
        public DbSet<OwnershipCost> OwnershipCost { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-UN9Q6TI;Database=fin-accounting-api;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(MaintenanceCostConfig).Assembly);
            base.OnModelCreating(builder);
        }
    }
}

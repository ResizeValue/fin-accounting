using FinAccountingApi.Domain.PaymentCheck;
using FinAccountingApi.Domain.Resources;
using FinAccountingApi.Domain.Resources.Configs;
using FinAccountingApi.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinAccountingApi.Persistance
{
    public class FinAccountingApiContext : IdentityDbContext<ApiUser>
    {
        public FinAccountingApiContext()
        {
        }

        public FinAccountingApiContext(DbContextOptions<FinAccountingApiContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<ApiUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserResource> UserResources { get; set; }
        public DbSet<OwnershipCost> OwnershipCost { get; set; }
        public DbSet<PaymentCheck> PaymentChecks { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(MaintenanceCostConfig).Assembly);
            base.OnModelCreating(builder);
        }
    }
}

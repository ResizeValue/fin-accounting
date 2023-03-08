using FinAccountingApi.Application.Accounts;
using FinAccountingApi.Application.Resources;
using FinAccountingApi.Application.Resources.Mapper;
using FinAccountingApi.Application.Users;
using FinAccountingApi.Application.Users.Mapper;
using FinAccountingApi.Domain.Users;
using FinAccountingApi.Persistance;
using FinAccountingApi.Persistance.Resources;
using FinAccountingApi.Persistance.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinAccountingApi.InfrastructureIoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IUserRepository), typeof(UsersRepository));
            services.AddScoped(typeof(IResourceRepository), typeof(ResourcesRepository));

            services.AddScoped<UserService>();
            services.AddScoped<ResourceService>();
            services.AddScoped<AccountsService>();
            services.AddScoped<UserMapper>();
            services.AddScoped<ResourceMapper>();
        }

        public static void RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<FinAccountingApiContext>(options => options.UseSqlServer(connectionString));
        }

        public static void RegisterIdentity(IServiceCollection services, string connectionString)
        {
            services.AddIdentity<ApiUser, IdentityRole>(
                    option =>
                    {
                        option.Password.RequireDigit = false;
                        option.Password.RequiredLength = 6;
                        option.Password.RequireNonAlphanumeric = false;
                        option.Password.RequireUppercase = false;
                        option.Password.RequireLowercase = false;
                    }
                ).AddEntityFrameworkStores<FinAccountingApiContext>()
                .AddDefaultTokenProviders();
        }

        public static void InitialScopes(IServiceProvider provider)
        {
            var services = provider.CreateScope().ServiceProvider;

            var context = services.GetRequiredService<FinAccountingApiContext>();
            context.Database.Migrate();
        }
    }
}

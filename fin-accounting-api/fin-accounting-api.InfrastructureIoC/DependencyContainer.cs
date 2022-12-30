using fin_accounting_api.Application.Accounts;
using fin_accounting_api.Application.Resources;
using fin_accounting_api.Application.Resources.Mapper;
using fin_accounting_api.Application.Users;
using fin_accounting_api.Application.Users.Mapper;
using fin_accounting_api.Domain.Users;
using fin_accounting_api.Persistance;
using fin_accounting_api.Persistance.Resources;
using fin_accounting_api.Persistance.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace fin_accounting_api.InfrastructureIoC
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
            services.AddDbContext<Fin_accounting_apiContext>(options => options.UseSqlServer(connectionString));
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
                ).AddEntityFrameworkStores<Fin_accounting_apiContext>()
                .AddDefaultTokenProviders();
        }

        public static void InitialScopes(IServiceProvider provider)
        {
            var services = provider.CreateScope().ServiceProvider;

            var context = services.GetRequiredService<Fin_accounting_apiContext>();
            context.Database.Migrate();
        }
    }
}

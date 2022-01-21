using fin_accounting_api.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace fin_accounting_api.Data;

public class fin_accounting_apiContext : IdentityDbContext<apiUser>
{
    public fin_accounting_apiContext(DbContextOptions<fin_accounting_apiContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

using fin_accounting_api.Domain.Resources;
using Microsoft.AspNetCore.Identity;

namespace fin_accounting_api.Domain.Users
{
    public class ApiUser : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<UserResource> Resources { get; set; }
    }
}

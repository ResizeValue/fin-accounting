using FinAccountingApi.Domain.Resources;
using Microsoft.AspNetCore.Identity;

namespace FinAccountingApi.Domain.Users
{
    public class ApiUser : IdentityUser
    {
        public string Name { get; set; }

        public ICollection<UserResource> Resources { get; set; }
    }
}

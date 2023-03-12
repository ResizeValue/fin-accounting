using FinAccountingApi.Application.Resources;

namespace FinAccountingApi.Application.Users
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public IEnumerable<ResourceModel> Resources { get; set; }
    }
}

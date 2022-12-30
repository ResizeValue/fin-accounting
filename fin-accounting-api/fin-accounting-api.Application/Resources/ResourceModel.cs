using fin_accounting_api.Application.Resources.OwnershipCost;
using fin_accounting_api.Application.Users;
using fin_accounting_api.Domain.Resources;

namespace fin_accounting_api.Application.Resources
{
    public class ResourceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public decimal Cost { get; set; }
        public string? Description { get; set; }
        public IEnumerable<OwnershipCostModel> OwnershipCost { get; set; }
        public int? ParentId { get; set; }
        public IEnumerable<ResourceModel> Resources { get; set; }
        public string Image { get; set; }
        public UserModel Owner { get; set; }
    }
}

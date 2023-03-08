using FinAccountingApi.Application.Resources.OwnershipCost;
using FinAccountingApi.Application.Users;
using FinAccountingApi.Domain.Resources;

namespace FinAccountingApi.Application.Resources
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

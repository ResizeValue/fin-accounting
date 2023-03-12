using FinAccountingApi.Domain.Resources;

namespace FinAccountingApi.Application.Resources.OwnershipCost
{
    public class OwnershipCostModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public OwnershipCostPeriodicityEnum Periodicity { get; set; }

        public string? Description { get; set; }

        public ResourceModel Resource { get; set; }
    }
}

namespace FinAccountingApi.Domain.Resources
{
    public class OwnershipCost
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public OwnershipCostPeriodicityEnum Periodicity { get; set; }

        public string? Description { get; set; }

        public UserResource Resource { get; set; }
    }
}

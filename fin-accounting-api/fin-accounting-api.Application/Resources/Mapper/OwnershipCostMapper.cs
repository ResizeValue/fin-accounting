using fin_accounting_api.Application.Resources.OwnershipCost;

namespace fin_accounting_api.Application.Resources.Mapper
{
    public class OwnershipCostMapper
    {
        public static IEnumerable<OwnershipCostModel> GetOwnershipCostModelCollection(ICollection<Domain.Resources.OwnershipCost> cost)
        {
            if(cost == null)
            {
                return null;
            }

            return cost.Select(cost => new OwnershipCostModel
            {
                Id = cost.Id,
                Description = cost.Description,
                Cost = cost.Cost,
                Name = cost.Name,
                Periodicity = cost.Periodicity
            });
        }
    }
}

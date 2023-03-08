using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinAccountingApi.Domain.Resources.Configs
{
    public class MaintenanceCostConfig : IEntityTypeConfiguration<OwnershipCost>
    {
        public void Configure(EntityTypeBuilder<OwnershipCost> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}

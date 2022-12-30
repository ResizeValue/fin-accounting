using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fin_accounting_api.Domain.Resources.Configs
{
    public class MaintenanceCostConfig : IEntityTypeConfiguration<OwnershipCost>
    {
        public void Configure(EntityTypeBuilder<OwnershipCost> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}

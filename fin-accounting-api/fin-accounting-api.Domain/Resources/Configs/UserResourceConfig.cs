using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinAccountingApi.Domain.Resources.Configs
{
    public class UserResourceConfig : IEntityTypeConfiguration<UserResource>
    {
        public void Configure(EntityTypeBuilder<UserResource> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}

using FinAccountingApi.Domain.Resources;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FinAccountingApi.Domain.PaymentCheck.Configs
{
    public class PaymentCheckConfig : IEntityTypeConfiguration<PaymentCheck>
    {
        public void Configure(EntityTypeBuilder<PaymentCheck> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.User);
        }
    }
}

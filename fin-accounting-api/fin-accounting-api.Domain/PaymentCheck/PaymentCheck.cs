using FinAccountingApi.Domain.Users;

namespace FinAccountingApi.Domain.PaymentCheck
{
    public class PaymentCheck
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public DateTime Date { get; set; }

        public virtual IReadOnlyCollection<Product>? Products { get; set; }

        public ApiUser User { get; set; }
    }
}

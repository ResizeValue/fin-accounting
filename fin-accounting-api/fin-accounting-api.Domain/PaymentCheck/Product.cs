namespace FinAccountingApi.Domain.PaymentCheck
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ICollection<Tag>? Tags { get; set; }
    }
}

using FinAccountingApi.Application.PaymentChecks.Products.Tags;

namespace FinAccountingApi.Application.PaymentChecks.Products
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<TagModel>? Tags { get; set; }
    }
}

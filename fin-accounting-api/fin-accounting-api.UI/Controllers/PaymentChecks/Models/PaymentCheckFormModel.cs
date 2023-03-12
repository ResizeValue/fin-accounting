namespace FinAccountingApi.Presentation.Controllers.PaymentChecks.Models
{
    public class PaymentCheckFormModel
    {
        public string? Id { get; set; }

        public string Name { get; set; }

        public DateTime? Date { get; set; }

        public IEnumerable<ProductFormModel>? Products { get; set; }

        public string UserId { get; set; }
    }

    public class ProductFormModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}

using FinAccountingApi.Application.PaymentChecks.Products;
using FinAccountingApi.Application.Users;

namespace FinAccountingApi.Application.PaymentChecks
{
    public class PaymentCheckModel
    {
        public string Id { get; set; }

        public string? Name { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<ProductModel>? Products { get; set; }

        public UserModel? User { get; set; }
    }
}

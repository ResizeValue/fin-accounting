using FinAccountingApi.Application.PaymentChecks.Products;
using FinAccountingApi.Domain.PaymentCheck;

namespace FinAccountingApi.Application.PaymentChecks
{
    public class PaymentCheckMapper
    {
        public static PaymentCheckModel GetPaymentCheckModel(PaymentCheck payment)
        {
            return new PaymentCheckModel
            {
                Id = payment.Id.ToString(),
                Name = payment.Name,
                Date = payment.Date,
                Products = ProductMapper.GetListProductModel(payment.Products)
            };
        }

        public static IEnumerable<PaymentCheckModel>? GetListPaymentCheckModel(IEnumerable<PaymentCheck>? products)
        {
            if (products == null) return null;

            return products.Select(x => GetPaymentCheckModel(x));
        }

        public static PaymentCheck GetPaymentCheck(PaymentCheckModel payment)
        {
            return new PaymentCheck
            {
                Id = string.IsNullOrWhiteSpace(payment.Id) ? Guid.Parse(payment.Id) : Guid.Empty,
                Name = payment.Name,
                Date = payment.Date,
                Products = ProductMapper.GetListProduct(payment.Products)?.ToList()
            };
        }

        public static IEnumerable<PaymentCheck>? GetListPaymentCheck(IEnumerable<PaymentCheckModel>? products)
        {
            if (products == null) return null;

            return products.Select(x => GetPaymentCheck(x));
        }
    }
}
